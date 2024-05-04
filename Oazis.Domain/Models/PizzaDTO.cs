using System.Collections.Generic;

namespace Oazis.Domain.Models
{
    public class PizzaDTO
    {
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int SerialNumber { get; set; }
        public IEnumerable<string> Ingredients { get; set; }
    }
}
