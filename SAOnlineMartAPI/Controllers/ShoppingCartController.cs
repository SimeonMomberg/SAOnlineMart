using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAOnlineMartAPI.Data;
using SAOnlineMartAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAOnlineMartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShoppingCartController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingCart/5/GetCart
        [HttpGet("{userId}/GetCart")]
        public async Task<ActionResult<ShoppingCartDto>> GetCart(int userId)
        {
            var cartItems = await _context.ShoppingCarts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return NotFound(new { message = "Cart not found for the specified user." });
            }

            var shoppingCartDto = new ShoppingCartDto
            {
                UserId = userId,
                Items = cartItems.Select(c => new ShoppingCartItemDto
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    Price = c.Price
                }).ToList()
            };

            return Ok(shoppingCartDto);
        }

        // POST: api/ShoppingCart/5/add
        [HttpPost("{userId}/add")]
        public async Task<IActionResult> AddToCart(int userId, [FromBody] ShoppingCartItemDto itemDto)
        {
            if (itemDto == null)
            {
                return BadRequest(new { message = "Invalid item data." });
            }

            var existingCartItem = await _context.ShoppingCarts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == itemDto.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += itemDto.Quantity;
                existingCartItem.Price += itemDto.Price;
            }
            else
            {
                var cartItem = new ShoppingCart
                {
                    UserId = userId,
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    Price = itemDto.Price,
                    CreatedDate = DateTime.Now
                };

                _context.ShoppingCarts.Add(cartItem);
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Item added to cart." });
        }

        // PUT: api/ShoppingCart/5/update
        [HttpPut("{userId}/update")]
        public async Task<IActionResult> UpdateCartItem(int userId, [FromBody] ShoppingCartItemDto itemDto)
        {
            if (itemDto == null)
            {
                return BadRequest(new { message = "Invalid item data." });
            }

            var cartItem = await _context.ShoppingCarts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == itemDto.ProductId);

            if (cartItem == null)
            {
                return NotFound(new { message = "Cart item not found." });
            }

            cartItem.Quantity = itemDto.Quantity;
            cartItem.Price = itemDto.Price;

            _context.Entry(cartItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/ShoppingCart/5/remove/10
        [HttpDelete("{userId}/remove/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int productId)
        {
            var cartItem = await _context.ShoppingCarts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem == null)
            {
                return NotFound(new { message = "Cart item not found." });
            }

            _context.ShoppingCarts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}