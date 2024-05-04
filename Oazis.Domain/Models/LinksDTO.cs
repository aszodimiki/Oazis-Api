using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oazis.Domain.Models
{
    public class LinksDTO
    {
        public string Name { get; set; }
        public string? Url { get; set; }
        public IEnumerable<LinksDTO> NestedLinks { get; set; }
    }
}
