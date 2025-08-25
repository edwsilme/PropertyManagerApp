using MillionManagerApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionManagerApp.Domain.Interfaces
{
    public interface IPropertyImageRepository
    {
        Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(string propertyId);
        Task<IEnumerable<PropertyImage>> GetByPropertyIdsAsync(IEnumerable<string> propertyIds);
    }
}
