namespace projektSBD.Models
{
    public class Car
    {
        public int CarID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
        public decimal? EngineSize { get; set; }

        public ICollection<CarOwner> CarOwners { get; set; }

        public ICollection<ServiceHistory> ServiceHistories { get; set; }

        public ICollection<Accidents> Accidents { get; set; }
    }
}
