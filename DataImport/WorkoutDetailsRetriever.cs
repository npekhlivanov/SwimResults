namespace DataImport
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    public sealed class WorkoutDetailsRetriever
    {
        public static async Task<Stream> DownloadWorkoutDetails(string serviceWebAddress, int workoutId)
        {
            using var httpClient = new HttpClient 
            { 
                BaseAddress = new Uri(serviceWebAddress), 
                Timeout = TimeSpan.FromSeconds(10) 
            };
            using var responseMessage = await httpClient.GetAsync(new Uri($"{serviceWebAddress}{workoutId}"))
                .ConfigureAwait(false);
            if (!responseMessage.IsSuccessStatusCode || responseMessage.Content.Headers.ContentType.MediaType != "text/xml")
            {
                return null;
            }

            var result = new MemoryStream();
            using (var responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
            {
                await responseStream.CopyToAsync(result).ConfigureAwait(false);
            }

            result.Position = 0;
            return result;
        }
    }
}
