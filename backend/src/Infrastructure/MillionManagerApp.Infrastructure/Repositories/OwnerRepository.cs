using MillionManagerApp.Domain.Entities;
using MillionManagerApp.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionManagerApp.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IMongoCollection<Owner> _owners;

        public OwnerRepository(IMongoDatabase database)
        {
            _owners = database.GetCollection<Owner>("Owner");
        }

        public async Task<Owner> GetByIdOwnerAsync(string idOwner)
        {
            return await _owners.Find(o => o.IdOwner == idOwner).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Owner>> GetOwnersByIdsAsync(IEnumerable<string> ids)
        {
            var filter = Builders<Owner>.Filter.In(o => o.IdOwner, ids);
            return await _owners.Find(filter).ToListAsync();
        }
    }
}
