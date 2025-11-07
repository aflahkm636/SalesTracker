namespace SalesTracker.models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<Sale> Sale { get; set; }

    }
}
