using MillionManagerApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionManagerApp.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property> GetByIdAsync(string id);
        Task<IEnumerable<Property>> GetFilteredAsync(string name, string address, double? minPrice, double? maxPrice);
    }
}
