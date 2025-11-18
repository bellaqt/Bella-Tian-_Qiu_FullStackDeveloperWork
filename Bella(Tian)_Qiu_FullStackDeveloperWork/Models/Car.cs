namespace Bella_Tian__Qiu_FullStackDeveloperWork.Models
{
    public class Car
    {
        public int Id { get; set; }
        //brand of the car
        public string Make { get; set; } = "";
        public string Owner { get; set; } = "";
        public string LicenseNumber { get; set; } = "";
        public DateTime RegistrationExpiry { get; set; }
    }
}
