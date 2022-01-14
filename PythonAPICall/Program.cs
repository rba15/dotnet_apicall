using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientSample
{
    public class Response
    {
        public string output { get; set; }
        public string error { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }
        static async Task RunAsync()
        {
            try
            {
                // Get the product
                var response = await client.GetAsync("http://10.30.31.102:5000/run/hello");
                int statusCode = (int) response.StatusCode;
                Console.WriteLine($"response: {response})");
                if(statusCode == 200) // success
                {
                    Response body = await response.Content.ReadAsAsync<Response>();
                    Console.WriteLine($"output: {body.output}");
                    Console.WriteLine($"error message: {body.error}");

                }
                else if(statusCode == 500) // internal server error
                {
                    Response body = await response.Content.ReadAsAsync<Response>();
                    Console.WriteLine($"An error has occurred, error message: {body.error}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}