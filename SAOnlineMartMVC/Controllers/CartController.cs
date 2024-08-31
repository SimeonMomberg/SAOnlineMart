using Microsoft.AspNetCore.Mvc;
using SAOnlineMart.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SAOnlineMart.Models;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IProductService _productService; // Added for fetching product details
    private readonly ILogger<CartController> _logger;

    public CartController(ICartService cartService, IProductService productService, ILogger<CartController> logger)
    {
        _cartService = cartService;
        _productService = productService; // Initialize product service
        _logger = logger;
    }

    private bool IsUserAuthenticated()
    {
        return !string.IsNullOrEmpty(HttpContext.Session.GetString("JWToken"));
    }

    public async Task<IActionResult> Index()
    {
        if (!IsUserAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        if (userId == 0)
        {
            return RedirectToAction("Login", "Account");
        }

        try
        {
            var cart = await _cartService.GetCartAsync(userId);
            return View(cart);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the cart for user {UserId}", userId);
            ViewBag.ErrorMessage = "There was a problem retrieving your cart. Please try again later.";
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(int productId, int quantity)
    {
        if (!IsUserAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        if (userId == 0)
        {
            return RedirectToAction("Login", "Account");
        }

        try
        {
            // Get product price
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction(nameof(Index));
            }

            var item = new ShoppingCartItemDTO
            {
                ProductId = productId,
                Quantity = quantity,
                Price = product.Price
            };

            await _cartService.AddToCartAsync(userId, item);
            TempData["SuccessMessage"] = "Item added to cart.";
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while adding the item to the cart for user {UserId}", userId);
            TempData["ErrorMessage"] = "There was a problem adding the item to your cart. Please try again later.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Update(int productId, int quantity)
    {
        if (!IsUserAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        if (userId == 0)
        {
            return RedirectToAction("Login", "Account");
        }

        try
        {
            // Get product price
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction(nameof(Index));
            }

            var item = new ShoppingCartItemDTO
            {
                ProductId = productId,
                Quantity = quantity,
                Price = product.Price
            };

            await _cartService.UpdateCartAsync(userId, item);
            TempData["SuccessMessage"] = "Cart updated successfully.";
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while updating the cart for user {UserId}", userId);
            TempData["ErrorMessage"] = "There was a problem updating your cart. Please try again later.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int productId)
    {
        if (!IsUserAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        if (userId == 0)
        {
            return RedirectToAction("Login", "Account");
        }

        try
        {
            await _cartService.RemoveFromCartAsync(userId, productId);
            TempData["SuccessMessage"] = "Item removed from cart.";
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while removing the item from the cart for user {UserId}", userId);
            TempData["ErrorMessage"] = "There was a problem removing the item from your cart. Please try again later.";
        }

        return RedirectToAction(nameof(Index));
    }
}