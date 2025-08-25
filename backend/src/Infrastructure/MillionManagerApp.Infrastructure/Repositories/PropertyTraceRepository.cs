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
    public class PropertyTraceRepository : IPropertyTraceRepository
    {
        private readonly IMongoCollection<PropertyTrace> _traces;

        public PropertyTraceRepository(IMongoDatabase database)
        {
            _traces = database.GetCollection<PropertyTrace>("PropertyTrace");
        }

        public async Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(string propertyId)
        {
            var filter = Builders<PropertyTrace>.Filter.Eq(trace => trace.IdProperty, propertyId);
            return await _traces.Find(filter).ToListAsync();
        }
    }
}
