using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> SearchAsync(string query) {
            if (string.IsNullOrEmpty(query)) {
                return View("Index", new List<SearchResult>());
            }

            var client = new HttpClient();
            var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/users?q={query}");

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();
            var searchResults = await JsonSerializer.DeserializeAsync<List<SearchResult>>(responseStream);

            return View("_SearchResults", searchResults);
        }
    }

    public class SearchResult {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
    }
}
