using System.ComponentModel.DataAnnotations.Schema;

namespace SalesTracker.models
{
    public class Sale
    {
        public int saleId { get; set; }

        public int productId { get; set; }

        public Product product { get; set; }

        public int Quantity { get; set; }

        public DateTime date { get; set; }= DateTime.Now;

    }
}
