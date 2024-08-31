using Microsoft.AspNetCore.Mvc;
using SAOnlineMart.Services;
using System.Threading.Tasks;
using SAOnlineMart.Models;
using Microsoft.AspNetCore.Http;

public class AdminController : Controller
{
    private readonly IProductService _productService;

    public AdminController(IProductService productService)
    {
        _productService = productService;
    }

    private bool IsUserAuthenticated()
    {
        return !string.IsNullOrEmpty(HttpContext.Session.GetString("JWToken"));
    }

    private bool IsUserAdmin()
    {
        var role = HttpContext.Session.GetString("UserRole");
        return role != null && role.Equals("admin", StringComparison.OrdinalIgnoreCase);
    }

    public async Task<IActionResult> Index()
    {
        if (!IsUserAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!IsUserAdmin())
        {
            return RedirectToAction("Index", "Home");
        }

        var products = await _productService.GetAllProductsAsync();
        return View(products);
    }

    public IActionResult Create()
    {
        if (!IsUserAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!IsUserAdmin())
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductsDTO product)
    {
        if (!IsUserAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!IsUserAdmin())
        {
            return RedirectToAction("Index", "Home");
        }

        await _productService.CreateProductAsync(product);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (!IsUserAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!IsUserAdmin())
        {
            return RedirectToAction("Index", "Home");
        }

        var product = await _productService.GetProductByIdAsync(id);
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductsDTO product)
    {
        if (!IsUserAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!IsUserAdmin())
        {
            return RedirectToAction("Index", "Home");
        }

        await _productService.UpdateProductAsync(product);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (!IsUserAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!IsUserAdmin())
        {
            return RedirectToAction("Index", "Home");
        }

        await _productService.DeleteProductAsync(id);
        return RedirectToAction("Index");
    }
}