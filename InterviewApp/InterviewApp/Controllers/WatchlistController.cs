using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InterviewApp.BLL.Models.Watchlist;
using InterviewApp.BLL.Services.Interfaces;
using InterviewApp.Constants;
using InterviewApp.ViewModels.Watchlist;
using Microsoft.AspNetCore.Mvc;

namespace InterviewApp.Controllers
{
    public class WatchlistController : ApiControllerBase
    {
        private readonly IWatchlistService _watchlistService;

        public WatchlistController(IMapper mapper, 
            IWatchlistService watchlistService) : base(mapper)
        {
            _watchlistService = watchlistService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWatchlistItems(UpsertWatchlistItemsViewModel watchlistItem)
        {
            var createWatchlistItem = Mapper.Map<UpsertWatchlistItemsModel>(watchlistItem);
            await _watchlistService.CreateWatchlistItemsAsync(createWatchlistItem);

            return Ok(ResponseConstants.DefaultOkStringResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetWatchlistItemsForUser(int userId)
        {
            var watchlistModels = await _watchlistService.GetWatchlistItemsForUserAsync(userId);
            var watchlistViewModels = Mapper.Map<List<WatchlistItemViewModel>>(watchlistModels);

            return Ok(watchlistViewModels);
        }

        [HttpPut]
        public async Task<IActionResult> MarkFilmsAsWatched(UpsertWatchlistItemsViewModel watchlistItem)
        {
            var watchlistItemModel = Mapper.Map<UpsertWatchlistItemsModel>(watchlistItem);
            await _watchlistService.MarkFilmsAsWatchedAsync(watchlistItemModel);

            return Ok(ResponseConstants.DefaultOkStringResponse);
        }
    }
}
