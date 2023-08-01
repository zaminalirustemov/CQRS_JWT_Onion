using CQRS_JWTApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace CQRS_JWTApp.MVC.Controllers
{
    [Authorize(Roles = "SuperAdmin,Member")]
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
                HttpResponseMessage response = await client.GetAsync($"http://localhost:5281/api/categories");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    JsonSerializerOptions options = new()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var data = JsonSerializer.Deserialize<List<CategoryListModel>>(jsonData, options);

                    productCreateModel.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(data, "Id", "Definition");

                    return View(productCreateModel);
                }
            }

            return RedirectToAction(nameof(List));
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateModel productCreateModel)
        {
            var data = TempData["Categories"]?.ToString();
            if (data is not null)
            {
                var categories = JsonSerializer.Deserialize<List<SelectListItem>>(data);
                productCreateModel.Categories = new SelectList(categories, "Value", "Text");
            }

            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token is not null)
                {
                    HttpClient client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    var jsonData = JsonSerializer.Serialize(productCreateModel);

                    StringContent content = new(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"http://localhost:5281/api/products", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(List));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error!");
                    }
                }
            }
            return View(productCreateModel);
        }

        public async Task<IActionResult> Update(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token is not null)
            {
                HttpClient client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);



                HttpResponseMessage responseProduct = await client.GetAsync($"http://localhost:5281/api/products/{id}");

                if (responseProduct.IsSuccessStatusCode)
                {
                    var jsonData = await responseProduct.Content.ReadAsStringAsync();

                    JsonSerializerOptions jsonSerializerOptions = new()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var result = JsonSerializer.Deserialize<ProductUpdateModel>(jsonData, jsonSerializerOptions);

                    HttpResponseMessage responseCategory = await client.GetAsync($"http://localhost:5281/api/categories");

                    if (responseCategory.IsSuccessStatusCode)
                    {
                        var jsonCategoryData = await responseCategory.Content.ReadAsStringAsync();

                        JsonSerializerOptions options = new()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        };

                        var data = JsonSerializer.Deserialize<List<CategoryListModel>>(jsonCategoryData, options);
                        if (result is not null)
                            result.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(data, "Id", "Definition");
                    }

                    return View(result);
                }
            }
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateModel productUpdateModel)
        {
            var data = TempData["Categories"]?.ToString();
            if (data is not null)
            {
                var categories = JsonSerializer.Deserialize<List<SelectListItem>>(data);
                productUpdateModel.Categories = new SelectList(categories, "Value", "Text", productUpdateModel.CategoryId);
            }

            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token is not null)
                {
                    HttpClient client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    var jsonData = JsonSerializer.Serialize(productUpdateModel);

                    StringContent content = new(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"http://localhost:5281/api/products", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(List));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error!");
                    }
                }
            }
            return View(productUpdateModel);
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
