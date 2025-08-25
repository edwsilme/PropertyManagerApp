using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionManagerApp.Application.Dto
{
    public class PropertyDto
    {
        public string IdProperty { get; set; }
        public string IdOwner { get; set; }
        public string Name { get; set; }
        public string AddressProperty { get; set; }
        public double PriceProperty { get; set; }
        public string ImageUrl { get; set; }
    }
}