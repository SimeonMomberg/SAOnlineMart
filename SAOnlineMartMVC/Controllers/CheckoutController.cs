using Microsoft.AspNetCore.Mvc;
using SAOnlineMart.Services;
using System.Threading.Tasks;
using SAOnlineMart.Models;
using Microsoft.Extensions.Logging;

public class CheckoutController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;
    private readonly ILogger<CheckoutController> _logger;

    public CheckoutController(ICartService cartService, IOrderService orderService, ILogger<CheckoutController> logger)
    {
        _cartService = cartService;
        _orderService = orderService;
        _logger = logger;
    }

    private bool IsUserAuthenticated()
    {
        return !string.IsNullOrEmpty(HttpContext.Session.GetString("JWToken"));
    }

    // GET: /Checkout
    [HttpGet]
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

        var cart = await _cartService.GetCartAsync(userId);
        if (cart == null || !cart.Items.Any())
        {
            TempData["ErrorMessage"] = "Your cart is empty.";
            return RedirectToAction("Index", "Cart");
        }

        return View(cart); // Assuming you have a checkout view that takes a ShoppingCartDTO model
    }

    // POST: /Checkout/Complete
    [HttpPost]
    public async Task<IActionResult> Complete(string shippingAddress, string paymentMethod)
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
            // Retrieve the cart items
            var cart = await _cartService.GetCartAsync(userId);

            if (cart == null || !cart.Items.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            // Add cart items to orders
            await _orderService.CreateOrderAsync(userId, cart.Items, shippingAddress, paymentMethod);

            // Clear the cart
            await _cartService.ClearCartAsync(userId);

            TempData["SuccessMessage"] = "Purchase completed successfully!";
            return RedirectToAction("Confirmation");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while completing the purchase for user {UserId}", userId);
            TempData["ErrorMessage"] = "There was an issue completing your purchase. Please try again.";
            return RedirectToAction("Index", "Cart");
        }
    }

    // GET: /Checkout/Confirmation
    public IActionResult Confirmation()
    {
        if (!IsUserAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        return View();
    }
}