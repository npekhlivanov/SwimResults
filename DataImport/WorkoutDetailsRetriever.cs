namespace DataImport
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    public sealed class WorkoutDetailsRetriever
    {
        public static async Task<Stream> DownloadWorkoutDetails(string serviceUrl, int workoutId)
        {
            using var httpClient = new HttpClient { BaseAddress = new Uri(serviceUrl), Timeout = TimeSpan.FromSeconds(10) };
            using var responseMessage = await httpClient.GetAsync($"{serviceUrl}{workoutId}").ConfigureAwait(false);
            if (!responseMessage.IsSuccessStatusCode || responseMessage.Content.Headers.ContentType.MediaType != "text/xml")
            {
                return null;
            }

            var result = new MemoryStream();
            using (var responseStream = await responseMessage.Content.ReadAsStreamAsync())
            {
                await responseStream.CopyToAsync(result);
            }

            result.Position = 0;
            return result;
        }
    }
}
