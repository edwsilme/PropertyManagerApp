using Microsoft.AspNetCore.Mvc;
using MillionManagerApp.Application.Services;

namespace MillionManagerApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyApiController : ControllerBase
    {
        private readonly PropertyService _propertyService;

        public PropertyApiController(PropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProperties(
            [FromQuery] string? name,
            [FromQuery] string? address,
            [FromQuery] double? minPrice,
            [FromQuery] double? maxPrice)
        {
            try
            {
                var result = await _propertyService.GetProperties(name, address, minPrice, maxPrice);
            return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Errror", ex);
            }
        }

        [HttpGet("{Id}/details")]
        public async Task<IActionResult> GetPropertyDetails(string Id)
        {
            try
            {
                var details = await _propertyService.GetPropertyDetails(Id);
                if (details == null)
                    return NotFound(new { message = "Property not found" });

                return Ok(details);
            }
            catch (Exception ex)
            {
                throw new Exception("Errror", ex);
            }
        }
    }
}
