using MillionManagerApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionManagerApp.Domain.Interfaces
{
    public interface IOwnerRepository
    {
        Task<Owner> GetByIdOwnerAsync(string idOwner);
        Task<IEnumerable<Owner>> GetOwnersByIdsAsync(IEnumerable<string> ids);
    }
}
