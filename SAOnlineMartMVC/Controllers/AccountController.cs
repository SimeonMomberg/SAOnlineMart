using Microsoft.AspNetCore.Mvc;
using SAOnlineMart.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

public class AccountController : Controller
{
    private readonly HttpClient _httpClient;

    public AccountController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET: /Account/Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Account/Register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Map the RegisterViewModel to the API model
            var apiModel = new
            {
                name = model.Name,
                email = model.Email,
                passwordHash = model.PasswordHash
            };

            // Use an absolute URI directly for now
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7077/api/Auth/register", apiModel);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                // Parse response data as a JObject to safely access the "message" property
                var result = JObject.Parse(responseData);

                if (result.ContainsKey("message"))
                {
                    TempData["SuccessMessage"] = result["message"].ToString();
                }
                else
                {
                    TempData["SuccessMessage"] = "Registration successful.";
                }

                return RedirectToAction("Login");
            }
            else
            {
                // Handle the error response
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Registration failed: {errorMessage}");
            }
        }

        // If we get here, something failed; redisplay form
        return View(model);
    }

    // GET: /Account/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var apiModel = new
            {
                email = model.Email,
                password = model.Password
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7077/api/Auth/login", apiModel);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

                // Store token, userId, and role in session
                HttpContext.Session.SetString("JWToken", tokenResponse.Token);
                HttpContext.Session.SetInt32("UserId", tokenResponse.UserId);
                HttpContext.Session.SetString("UserRole", tokenResponse.Role);

                // Redirect based on role
                if (tokenResponse.Role == "Admin")
                {
                    return RedirectToAction("Index", "Admin"); // Redirect to Admin page
                }
                else
                {
                    return RedirectToAction("Index", "Home"); // Redirect to Home page
                }
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Login failed: {errorMessage}");
            }
        }

        return View(model);
    }
    // GET: /Account/Logout
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}