using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace InterviewApp.BLL.Extension
{
    public static class HttpExtensions
    {
        public static async Task<T> MapHttpResponseToModelAsync<T>(this HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            
            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var searchResultModel = await JsonSerializer.DeserializeAsync<T>(contentStream);

            if (searchResultModel == null)
            {
                throw new Exception();
            }

            return searchResultModel;
        }
    }
}
