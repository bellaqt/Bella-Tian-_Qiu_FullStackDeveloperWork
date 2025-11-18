using Bella_Tian__Qiu_FullStackDeveloperWork.Models;

namespace Bella_Tian__Qiu_FullStackDeveloperWork.Services;

public class CarsStore
{
    public List<Car> Cars { get; } = new()
    {
        new Car { Id = 1, Make = "Volvo", Owner = "Bella", LicenseNumber = "1A 3D45U", RegistrationExpiry = DateTime.Now.AddDays(30) },
        //test expired car, so put the date in the past
        new Car { Id = 2, Make = "BMW", Owner = "Wendy", LicenseNumber = "4B 66UIA", RegistrationExpiry = DateTime.Now.AddDays(-10) }
    };
}
