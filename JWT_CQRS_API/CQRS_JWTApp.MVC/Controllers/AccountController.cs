using CQRS_JWTApp.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace CQRS_JWTApp.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _httpClientFactory.CreateClient();

                StringContent content = new(JsonSerializer.Serialize(userLoginModel), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://localhost:5281/api/Auth/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    string jsonData = await response.Content.ReadAsStringAsync();

                    JsonSerializerOptions jsonSerializerOptions = new()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    };

                    var tokenModel = JsonSerializer.Deserialize<JwtTokenResponseModel>(jsonData, jsonSerializerOptions);

                    if (tokenModel is not null)
                    {
                        JwtSecurityTokenHandler handler = new();

                        JwtSecurityToken token = handler.ReadJwtToken(tokenModel.Token);

                        List<Claim> claims = token.Claims.ToList();

                        if (tokenModel.Token is not null)
                            claims.Add(new Claim("accessToken", tokenModel.Token));

                        ClaimsIdentity claimsIdentity = new(claims, JwtBearerDefaults.AuthenticationScheme);

                        ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

                        AuthenticationProperties authenticationProperties = new()
                        {
                            ExpiresUtc = tokenModel.ExpireDate,
                            IsPersistent = true,
                        };

                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Istifadeci adi ve ya sifre yanlisdir");
                }

            }
            return View(userLoginModel);
        }
    }
}
