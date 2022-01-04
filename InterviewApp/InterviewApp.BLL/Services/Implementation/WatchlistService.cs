using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InterviewApp.ApiClients.Clients.Interfaces;
using InterviewApp.BLL.Extension;
using InterviewApp.BLL.Models.EmailMessages;
using InterviewApp.BLL.Models.Imdb.FilmInfo;
using InterviewApp.BLL.Models.Imdb.FilmPoster;
using InterviewApp.BLL.Models.Imdb.WikipediaFilmInfo;
using InterviewApp.BLL.Models.Watchlist;
using InterviewApp.BLL.Services.Interfaces;
using InterviewApp.DAL.DataModels.Watchlist;
using InterviewApp.DAL.Repositories.Interfaces;
using InterviewApp.DAL.UnitOfWork;
using MoreLinq.Extensions;
using Org.BouncyCastle.Security;

namespace InterviewApp.BLL.Services.Implementation
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IMapper _mapper;
        private readonly IInterviewAppUnitOfWork _unitOfWork;
        private readonly IWatchlistRepository _watchlistRepository;
        private readonly IImdbClient _imdbClient;
        //private readonly IEmailService _emailService;

        public WatchlistService(IMapper mapper, 
            IInterviewAppUnitOfWork unitOfWork, 
            IWatchlistRepository watchlistRepository, 
            IImdbClient imdbClient, 
            IEmailService emailService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _watchlistRepository = watchlistRepository;
            _imdbClient = imdbClient;
            //_emailService = emailService;
        }

        public async Task CreateWatchlistItemsAsync(UpsertWatchlistItemsModel watchlistItem)
        {
            var createWatchlistDataModel = _mapper.Map<UpsertWatchlistItemsDataModel>(watchlistItem);
            await _watchlistRepository.CreateWatchlistItemsAsync(createWatchlistDataModel);

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<WatchlistItemModel>> GetWatchlistItemsForUserAsync(int userId)
        {
            if (userId < 0)
            {
                throw new InvalidKeyException(userId.ToString());
            }

            var watchlistItemDataModels = await _watchlistRepository.GetWatchlistItemsForUserAsync(userId);
            var watchlistItemModels = _mapper.Map<List<WatchlistItemModel>>(watchlistItemDataModels);

            return watchlistItemModels;
        }

        public async Task MarkFilmsAsWatchedAsync(UpsertWatchlistItemsModel watchlistItem)
        {
            var watchlistItemUpdateDataModel = _mapper.Map<UpsertWatchlistItemsDataModel>(watchlistItem);
            await _watchlistRepository.MarkFilmsAsWatchedAsync(watchlistItemUpdateDataModel);

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<FilmEmailNotificationModel>> GetMostRatedFilmBetweenUnwatchedAndSendEmailAsync()
        {
            var allUnwatchedFilmsForAllUsers = await _watchlistRepository.GetAllUnwatchedFilmsAsync();

            var groupedWatchlistItemsByUsers = allUnwatchedFilmsForAllUsers
                .GroupBy(w => w.UserId)
                .Where(wg => wg.Count() > 3)
                .Batch(5);

            var result = new List<Task<FilmEmailNotificationModel>>();
            foreach (var batch in groupedWatchlistItemsByUsers)
            {
                var taskWithRates = batch.Select(GetMostRatedFilmAsync);

                var modelsWithRates = await Task.WhenAll(taskWithRates);

                var fullFilmsInfoTasks = modelsWithRates.Select(GetFullFilmInfoFormEmailAsync);

                result.AddRange(fullFilmsInfoTasks);
            }

            var filmsWithRates = await Task.WhenAll(result);

            return filmsWithRates.ToList();
            //var emailTask = filmsWithRates.Select(_emailService.SendEmailAsync);
            //await Task.WhenAll(emailTask);
        }

        private async Task<FilmEmailNotificationModel> GetMostRatedFilmAsync(IEnumerable<UserWatchlistItemDataModel> emailMessageNotifications)
        {
            var result = new List<FilmEmailNotificationModel>();
            foreach (var model in emailMessageNotifications)
            {
                var responseMessage = await _imdbClient.GetFilmInfoAsync(model.FilmId);
                var filmInfoModel = await responseMessage.MapHttpResponseToModelAsync<FilmInfoModel>();

                var rating = filmInfoModel.IMDbRating ?? filmInfoModel.IMDbRatingVotes ??
                    filmInfoModel.MetacriticRating ?? filmInfoModel.ContentRating ?? "0";

                result.Add(new FilmEmailNotificationModel
                {
                    UserId = model.UserId,
                    ImdbId = model.FilmId,
                    Title = filmInfoModel.Title,
                    ImdbRating = double.Parse(rating, CultureInfo.InvariantCulture)
                });
            }

            var maxFilmRating = result.Max(x => x.ImdbRating);

            var mostRatedFilm = result
                .First(f => Math.Abs(f.ImdbRating - maxFilmRating) == 0);

            return mostRatedFilm;
        }

        private async Task<FilmEmailNotificationModel> GetFullFilmInfoFormEmailAsync(
            FilmEmailNotificationModel filmModel)
        {
            var filmPosterResponse = await _imdbClient.GetFilmPosterAsync(filmModel.ImdbId);
            var posterResponseModel = await filmPosterResponse.MapHttpResponseToModelAsync<PosterResponseModel>();
            if (posterResponseModel.Posters != null && posterResponseModel.Posters.Any())
            {
                filmModel.PosterLink = posterResponseModel.Posters.First().Link;
            }

            var wikiResponse = await _imdbClient.GetDescriptionFromWikipediaAsync(filmModel.ImdbId);
            var wikiFilmResponseModel = await wikiResponse.MapHttpResponseToModelAsync<WikiFilmResponseModel>();
            if (wikiFilmResponseModel.PlotShort != null && !string.IsNullOrEmpty(wikiFilmResponseModel.PlotShort.Html))
            {
                filmModel.ShortDescriptionFromWiki = wikiFilmResponseModel.PlotShort.Html;
            }

            return filmModel;
        }
    }
}
