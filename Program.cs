using System.Net.Http.Headers;

namespace Upload.To.KiteWorks
{
    
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string csvFilePath = "path/to/your/file.csv"; // Replace with the actual path to your CSV file
            string kiteworksDestination = "/MyUploadedFiles"; // Replace with your desired destination in Kiteworks

            var uploader = new KiteworksUploader();
            bool success = await uploader.UploadCsvFileAsync(csvFilePath, kiteworksDestination);

            if (success)
            {
                Console.WriteLine("CSV file uploaded to Kiteworks successfully!");
            }
            else
            {
                Console.WriteLine("CSV file upload failed.");
            }
        }
    }

}
