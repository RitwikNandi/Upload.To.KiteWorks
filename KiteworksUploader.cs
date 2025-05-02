using System.Net.Http.Headers;

namespace Upload.To.KiteWorks
{
    public class KiteworksUploader
    {
        private readonly string _kiteworksUploadUrl = "YOUR_KITEWORKS_UPLOAD_ENDPOINT"; // Replace with the actual endpoint
        private readonly string _apiKey = "YOUR_API_KEY"; // Replace with your API key or authentication token and hide it

        public async Task<bool> UploadCsvFileAsync(string filePath, string destinationPath = "/")
        {
            using (var httpClient = new HttpClient())
            {
                // Set up authentication headers if required
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey); // Example using Bearer token

                using (var content = new MultipartFormDataContent())
                {
                    // Add the file to the form data
                    var fileStream = new StreamContent(File.OpenRead(filePath));
                    fileStream.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "file", // The name of the file parameter expected by the API
                        FileName = Path.GetFileName(filePath)
                    };
                    content.Add(fileStream);

                    // Add any other necessary parameters, like the destination path
                    content.Add(new StringContent(destinationPath), "path"); // Assuming the API expects a 'path' parameter

                    try
                    {
                        var response = await httpClient.PostAsync(_kiteworksUploadUrl, content);
                        response.EnsureSuccessStatusCode(); // Throws an exception for bad status codes

                        // Optionally process the response
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Upload successful. Response: {responseContent}");
                        return true;
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Error uploading file: {ex.Message}");
                        return false;
                    }
                }
            }
        }
    }

}
