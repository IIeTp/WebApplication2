using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class HomeController : Controller {
    private readonly HttpClient _client = new HttpClient();
    private const string ApiUrl = "https://api.ivi.ru/mobileapi/autocomplete/common/v7/";

    public IActionResult Index() {
        return View();
    }
    public IActionResult Privacy() {
        return View();
    }

    [HttpGet]
    public async Task<JsonResult> Search(string query) {
        var requestUrl = $"{ApiUrl}?query={query}&fields=id,name,object_type,title,year,years,orig_title&app_version=2182";
        var response = await _client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<SearchResult>(content);
        return Json(result.Results);
    }
}

public class SearchResult {
    public SearchResultItem[] Results { get; set; }
}

public class SearchResultItem {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Object_type { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Years { get; set; }
    public string Orig_title { get; set; }
}
