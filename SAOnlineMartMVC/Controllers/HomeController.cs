using Microsoft.AspNetCore.Mvc;
using SAOnlineMart.Services;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsAsync();
        return View(products);
    }
    public IActionResult Error()
    {
        return View();
    }

}