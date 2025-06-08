using System.ComponentModel.DataAnnotations.Schema;

namespace projektSBD.Models
{
    [Table("CARS")]
    public class Car
    {
        public int CARID { get; set; }
        public string BRAND { get; set; }
        public string MODEL { get; set; }
        public int YEAR { get; set; }
        public decimal PRICE { get; set; }
        public string FUELTYPE { get; set; }
        public int MILEAGE { get; set; }
        public string COLOR { get; set; }
        public decimal? ENGINESIZE { get; set; }

        public ICollection<CarOwner> CarOwners { get; set; }
        public ICollection<ServiceHistory> ServiceHistories { get; set; }
        public ICollection<Accidents> Accidents { get; set; }
        public ICollection<InsurancePolicy> InsurancePolicies { get; set; }
    }
}
