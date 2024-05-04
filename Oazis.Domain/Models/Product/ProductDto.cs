namespace Oazis.Domain.Models.Product
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int SerialNumber { get; set; }
        public IEnumerable<string> Ingredients { get; set; }
    }
}
