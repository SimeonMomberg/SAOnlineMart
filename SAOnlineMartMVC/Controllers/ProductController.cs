using Microsoft.AspNetCore.Mvc;
using SAOnlineMart.Services;
using SAOnlineMart.Models;
using System.Threading.Tasks;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: /Product/Details
    public async Task<IActionResult> Details(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // GET: /Product/Edit
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // POST: /Product/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductsDTO product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _productService.UpdateProductAsync(product);
            }
            catch (HttpRequestException e)
            {
                // Handle HTTP request exceptions
                ModelState.AddModelError("", $"Error updating product: {e.Message}");
                return View(product);
            }

            return RedirectToAction(nameof(Details), new { id = product.Id });
        }

        return View(product);
    }

    // GET: /Product/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Product/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductsDTO product)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _productService.CreateProductAsync(product);
            }
            catch (HttpRequestException e)
            {
                // Handle HTTP request exceptions
                ModelState.AddModelError("", $"Error creating product: {e.Message}");
                return View(product);
            }

            return RedirectToAction(nameof(Details), new { id = product.Id });
        }

        return View(product);
    }

    // GET: /Product/Delete
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // POST: /Product/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _productService.DeleteProductAsync(id);
        }
        catch (HttpRequestException e)
        {
            // Handle HTTP request exceptions
            ModelState.AddModelError("", $"Error deleting product: {e.Message}");
            return RedirectToAction(nameof(Delete), new { id });
        }

        return RedirectToAction(nameof(Index));
    }
}
