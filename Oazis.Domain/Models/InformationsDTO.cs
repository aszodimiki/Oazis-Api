using System.Collections.Generic;

namespace Oazis.Domain.Models
{
    public class InformationsDTO
    {
        public string Address { get; set; }
        public string Delivery { get; set; }
        public string Location { get; set; }
        public string Reservation { get; set; }
        public IEnumerable<string> PhoneNumbers { get; set; }
    }
}
