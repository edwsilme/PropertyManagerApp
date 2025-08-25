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
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly IMongoCollection<PropertyImage> _images;

        public PropertyImageRepository(IMongoDatabase database)
        {
            _images = database.GetCollection<PropertyImage>("PropertyImage");
        }

        public async Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(string propertyId)
        {
            var filter = Builders<PropertyImage>.Filter.Eq(img => img.IdProperty, propertyId);
            return await _images.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<PropertyImage>> GetByPropertyIdsAsync(IEnumerable<string> propertyIds)
        {
            var filter = Builders<PropertyImage>.Filter.In(img => img.IdProperty, propertyIds);
            return await _images.Find(filter).ToListAsync();
        }
    }
}
