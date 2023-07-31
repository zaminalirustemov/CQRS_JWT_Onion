using CQRS_JWTApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace CQRS_JWTApp.MVC.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> List()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token is not null)
            {
                HttpClient client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync("http://localhost:5281/api/categories");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    JsonSerializerOptions jsonSerializerOptions = new()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var result = JsonSerializer.Deserialize<List<CategoryListModel>>(jsonData, jsonSerializerOptions);

                    return View(result);
                }
            }

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateModel categoryCreateModel)
        {
            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token is not null)
                {
                    HttpClient client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    var jsonData = JsonSerializer.Serialize(categoryCreateModel);
                    StringContent content = new(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("http://localhost:5281/api/categories", content);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(List));
                    else
                        ModelState.AddModelError("", "Error!");
                }
            }
            return View(categoryCreateModel);
        }

        public async Task<IActionResult> Update(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token is not null)
            {
                HttpClient client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.GetAsync($"http://localhost:5281/api/categories/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    JsonSerializerOptions jsonSerializerOptions = new()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var result = JsonSerializer.Deserialize<CategoryListModel>(jsonData, jsonSerializerOptions);

                    return View(result);
                }
            }
            return RedirectToAction(nameof(List));
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryListModel categoryListModel)
        {
            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token is not null)
                {
                    HttpClient client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    var jsonData = JsonSerializer.Serialize(categoryListModel);
                    StringContent content = new(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync("http://localhost:5281/api/categories", content);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(List));
                    else
                        ModelState.AddModelError("", "Error!");
                }
            }
            return View(categoryListModel);
        }



        public async Task<IActionResult> Remove(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token is not null)
            {
                HttpClient client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client.DeleteAsync($"http://localhost:5281/api/categories/{id}");
            }

            return RedirectToAction(nameof(List));
        }
    }
}
