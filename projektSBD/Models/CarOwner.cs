namespace projektSBD.Models
{
    public class CarOwner
    {
        public int CarID { get; set; }
        public int OwnerID { get; set; }

        public Car Car { get; set; }

        public Owner Owner { get; set; }
    }
}
