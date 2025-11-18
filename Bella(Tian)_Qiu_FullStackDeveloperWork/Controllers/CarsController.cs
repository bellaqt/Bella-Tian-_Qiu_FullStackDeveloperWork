using Bella_Tian__Qiu_FullStackDeveloperWork.Models;
using Bella_Tian__Qiu_FullStackDeveloperWork.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bella_Tian__Qiu_FullStackDeveloperWork.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly CarsStore _store;

        public CarsController(CarsStore store)
        {
            _store = store;
        }

        [HttpGet]
        public IEnumerable<Car> Get(string? make)
        {
            var Cars = _store.Cars;

            if (!string.IsNullOrWhiteSpace(make))
                return Cars.Where(c => c.Make.Equals(make, StringComparison.OrdinalIgnoreCase));

            return Cars;
        }
    }
}
