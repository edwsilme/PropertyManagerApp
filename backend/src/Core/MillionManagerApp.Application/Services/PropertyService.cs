using MillionManagerApp.Application.Dto;
using MillionManagerApp.Domain.Entities;
using MillionManagerApp.Domain.Exceptions;
using MillionManagerApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionManagerApp.Application.Services
{
    public class PropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IPropertyImageRepository _imageRepository;
        private readonly IPropertyTraceRepository _traceRepository;

        public PropertyService(
            IPropertyRepository propertyRepository,
            IOwnerRepository ownerRepository,
            IPropertyImageRepository imageRepository,
            IPropertyTraceRepository traceRepository)
        {
            _propertyRepository = propertyRepository;
            _ownerRepository = ownerRepository;
            _imageRepository = imageRepository;
            _traceRepository = traceRepository;
        }

        public async Task<IEnumerable<PropertyDto>> GetProperties(string name, string address, double? minPrice, double? maxPrice)
        {
            var properties = await _propertyRepository.GetFilteredAsync(name, address, minPrice, maxPrice);
            var ownerIds = properties.Select(p => p.IdOwner).Distinct().ToList();
            var propertyIds = properties.Select(p => p.IdProperty).Distinct().ToList();

            var owners = await _ownerRepository.GetOwnersByIdsAsync(ownerIds);
            var ownerDict = owners.ToDictionary(o => o.IdOwner);

            var propertyImages = await _imageRepository.GetByPropertyIdsAsync(propertyIds);
            var imageDict = propertyImages
                .GroupBy(pi => pi.IdProperty)
                .ToDictionary(
                    g => g.Key,
                    g => g.FirstOrDefault(pi => pi.Enabled) ?? g.FirstOrDefault()
                );


            var propertyDtos = new List<PropertyDto>();

            foreach (var property in properties)
            {
                if (ownerDict.TryGetValue(property.IdOwner, out var owner))
                {
                    var imageUrl = imageDict.ContainsKey(property.IdProperty) ? 
                        imageDict[property.IdProperty].File : null;

                    propertyDtos.Add(new PropertyDto
                    {
                        IdProperty = property.IdProperty,
                        IdOwner = owner.IdOwner,
                        Name = property.Name,
                        AddressProperty = property.Address,
                        PriceProperty = property.Price,
                        ImageUrl = imageUrl
                    });
                }
            }

            return propertyDtos;
        }

        public async Task<PropertyDetailsDto> GetPropertyDetails(string IdProperty)
        {
            if (string.IsNullOrWhiteSpace(IdProperty))
                throw new ArgumentException("Property Id is required.", nameof(IdProperty));

            var property = await _propertyRepository.GetByIdAsync(IdProperty);

            if (property == null) 
                throw new NotFoundException($"Property with Id {IdProperty} not found.");

            var owner = await _ownerRepository.GetByIdOwnerAsync(property.IdOwner);
            var images = await _imageRepository.GetByPropertyIdAsync(property.IdProperty);
            var traces = await _traceRepository.GetByPropertyIdAsync(property.IdProperty);

            return new PropertyDetailsDto
            {
                IdProperty = property.IdProperty,
                Name = property.Name,
                Address = property.Address,
                Price = property.Price,
                CodeInternal = property.CodeInternal,
                Year = property.Year,
                Owner = owner == null ? null : new OwnerDto
                {
                    IdOwner = owner.IdOwner,
                    Name = owner.Name,
                    Address = owner.Address,
                    Photo = owner.Photo,
                    Birthday = owner.Birthday
                },
                Images = images.Select(img => new PropertyImageDto
                {
                    IdPropertyImage = img.IdPropertyImage,
                    File = img.File,
                    Enabled = img.Enabled
                }).ToList() ?? new List<PropertyImageDto>(),
                Traces = traces.Select(trace => new PropertyTraceDto
                {
                    IdPropertyTrace = trace.IdPropertyTrace,
                    DateSale = trace.DateSale,
                    Name = trace.Name,
                    Value = trace.Value,
                    Tax = trace.Tax
                }).ToList() ?? new List<PropertyTraceDto>()
            };
        }


    }
}
