namespace projektSBD.Models
{
    public class ServiceHistory
    {
        public int ServiceID { get; set; }
        public int CarID { get; set; }
        public DateTime ServiceDate { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        public Car Car { get; set; }
    }
}
