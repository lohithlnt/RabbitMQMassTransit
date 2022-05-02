namespace SharedLib.Models
{
    public class Basket
    {
        public Guid BasketId { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
    }
}
