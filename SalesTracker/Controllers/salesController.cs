using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesTracker.data;
using SalesTracker.models;
using SalesTracker.NewFolder;

namespace SalesTracker.Controllers
{
    [Route("api/controller")]
    [ApiController]

    public class salesController:ControllerBase
    {
        private readonly AppdbContext _context;

        public salesController(AppdbContext context)
        {
            _context = context;
        }

        [HttpPost]

        public async Task<IActionResult> CreateSale([FromBody] SaleCreateDto dto)
        {
            bool productExists = await _context.products
                .AnyAsync(p => p.Id == dto.ProductId);

            if (!productExists)
                return NotFound("no product found");

            var sale = new Sale
            {
                productId = dto.ProductId,
                Quantity = dto.Quantity,
                date=DateTime.UtcNow
            };


            _context.sale.Add(sale);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Sale addedd successfully" });
        }
        [HttpGet]
        public async Task<IActionResult> Getsales()
        {
            var sales = await _context.sale.ToListAsync();
            return Ok(sales);

        }
      

        [HttpGet("report/{date}")]
        public async Task<IActionResult> GetReport(DateTime date)
        {
            var sales = await _context.sale
      .Where(s => s.date.Date == date.Date)
      .Include(s => s.product)
      .ToListAsync();

            if (!sales.Any())
                return Ok(new { message = "No sales found in this date" });

            var topProduct = sales
                .GroupBy(s => new { s.productId, s.product.Name })
                .Select(g => new
                {
                    ProductName = g.Key.Name,
                    TotalSold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(g => g.TotalSold)
                .FirstOrDefault();

            var report = new
            {
                date = date.ToString("yyyy-MM-dd"),
                total_sales = sales.Sum(s => s.Quantity * s.product.Price),
                total_items_sold = sales.Sum(s => s.Quantity),
                top_product = topProduct?.ProductName
            };

            return Ok(report);

        }

     

    }
}
