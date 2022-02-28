// See https://aka.ms/new-console-template for more information
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient
{
    class Book
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("publish_date")]
        public string Published_Date { get; set; }

        [JsonProperty("number_of_pages")]
        public string Page_Number { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

    }

    
    class Program
    {
        private static readonly HttpClient client = new HttpClient();



        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()

        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Book ISBN#. Press Enter without writing anything to exit");
                    var ISBN = Console.ReadLine();
                    if (string.IsNullOrEmpty(ISBN))
                    {
                        break;
                    }
                    var result = await client.GetAsync("https://openlibrary.org/isbn/" + ISBN.ToLower() + ".json");
                    var resultRead = await result.Content.ReadAsStringAsync();
                    var book = JsonConvert.DeserializeObject<Book>(resultRead);

                    Console.WriteLine("____");
                    Console.WriteLine("Title: " + book.Title);
                    Console.WriteLine("Book Key: " + book.Key);
                    Console.WriteLine("First Published: " + book.Published_Date);
                    Console.WriteLine("Number of Pages: " + book.Page_Number);
                    Console.WriteLine("\n---");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error.Please Enter Vaild ISBN Name");

                }
            }
        }

    }

}

