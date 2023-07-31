using CQRS_JWTApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CQRS_JWTApp.MVC.Controllers
{
    [Authorize(Roles ="SuperAdmin,Member")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
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

                HttpResponseMessage response = await client.GetAsync("http://localhost:5281/api/products");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    JsonSerializerOptions jsonSerializerOptions = new()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var result = JsonSerializer.Deserialize<List<ProductListModel>>(jsonData, jsonSerializerOptions);

                    return View(result);
                }
            }

            return View();
        }


        public async Task<IActionResult> Create()
        {
            ProductCreateModel productCreateModel = new();
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token is not null)
            {
                HttpClient client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response= await client.GetAsync($"http://localhost:5281/api/categories");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    JsonSerializerOptions options = new()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var data=JsonSerializer.Deserialize<List<CategoryListModel>>(jsonData,options);

                    productCreateModel.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(data,"Id","Definition");
                    
                    return View(productCreateModel);
                }
            }

            return RedirectToAction(nameof(List));
        }



        public async Task<IActionResult> Remove(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token is not null)
            {
                HttpClient client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client.DeleteAsync($"http://localhost:5281/api/products/{id}");
            }

            return RedirectToAction(nameof(List));
        }
    }
}
