using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionManagerApp.Application.Dto
{
    public class PropertyDetailsDto
    {
        public string IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }

        public OwnerDto Owner { get; set; }
        public List<PropertyImageDto> Images { get; set; }
        public List<PropertyTraceDto> Traces { get; set; }
    }
}
