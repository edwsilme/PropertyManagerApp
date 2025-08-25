using MillionManagerApp.Domain.Entities;
using MillionManagerApp.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionManagerApp.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IMongoCollection<Property> _properties;

        public PropertyRepository(IMongoDatabase database)
        {
            _properties = database.GetCollection<Property>("Property");
        }

        public async Task<IEnumerable<Property>> GetFilteredAsync(string name, string address, double? minPrice, double? maxPrice)
        {
            var filterBuilder = Builders<Property>.Filter;
            var filters = new List<FilterDefinition<Property>>();

            if (!string.IsNullOrEmpty(name))
                filters.Add(filterBuilder.Eq(p => p.Name, name));

            if (!string.IsNullOrEmpty(address))
                filters.Add(filterBuilder.Eq(p => p.Address, address));

            if (minPrice.HasValue)
                filters.Add(filterBuilder.Gte(p => p.Price, minPrice.Value));

            if (maxPrice.HasValue)
                filters.Add(filterBuilder.Lte(p => p.Price, maxPrice.Value));

            var filter = filters.Any() ? filterBuilder.And(filters) : filterBuilder.Empty;

            return await _properties.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _properties.Find(prop => true).ToListAsync();
        }

        public async Task<Property> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                throw new FormatException($"El id '{id}' no es un ObjectId válido.");
            return await _properties.Find(prop => prop.IdProperty == id).FirstOrDefaultAsync();
        }

    }
}
