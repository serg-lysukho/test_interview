using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using InterviewApp.ApiClients.Clients.Interfaces;
using InterviewApp.ApiClients.Constants.Corezoid;
using InterviewApp.BLL.Extension;
using InterviewApp.BLL.Models.Corezoid;
using InterviewApp.BLL.Models.Imdb.SearchFilm;
using InterviewApp.BLL.Services.Interfaces;
using InterviewApp.Configuration;

namespace InterviewApp.BLL.Services.Implementation
{
    public class FilmService : IFilmService
    {
        private readonly IImdbClient _imdbClient;
        private readonly ICorezoidClient _corezoidClient;
        private readonly IInterviewAppConfiguration _configurationProvider;

        public FilmService(IImdbClient imdbClient,
            ICorezoidClient corezoidClient,
            IInterviewAppConfiguration configurationProvider)
        {
            _imdbClient = imdbClient;
            _corezoidClient = corezoidClient;
            _configurationProvider = configurationProvider;
        }

        public async Task<List<SearchFilmResultModel>> SearchFilmByNameAsync(string filmName)
        {
            if (string.IsNullOrEmpty(filmName))
            {
                throw new ArgumentNullException(nameof(filmName));
            }

            var httpResponseMessage = await _imdbClient.SearchByNameAsync(filmName);
            var searchResultModel = await httpResponseMessage.MapHttpResponseToModelAsync<SearchFilmResponseModel>();

            return searchResultModel.Results;
        }

        public async Task GetGenresCountForFilmNamesAsync(IEnumerable<string> filmNames)
        {
            var names = filmNames.ToList();
            
            var requestRoot = new Root
            {
                RequestBodies = new[]
                {
                    new RequestBodyModel
                    {
                        OperationType = CorezoidOperationTypeConstants.Create,
                        OperatedObjectType = CorezoidObjectTypeConstants.Task,
                        ProcessId = CorezoidProcessInfoConstants.ProcessId,
                        ProcessParameters = new RequestContentModel
                        {
                            FilmNames = names,
                            ImdbApiKey = _configurationProvider.ImdbApiKey
                        }
                    }
                }
            };
            
            var jsonBody = JsonSerializer.Serialize(requestRoot);
            await _corezoidClient.SearchByNameAsync(jsonBody);
        }
    }
}
