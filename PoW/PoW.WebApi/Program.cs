using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace PoW.WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://localhost:9000";
            
            using (WebApp.Start<Startup>(url))
            {
                HttpClient client = new HttpClient();

                Console.WriteLine($"Task Work Server started at: { url }");

                var response = client.GetAsync($"{ url }/api/taskwork/get").Result;
                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine();
            }
        }
    }
}
