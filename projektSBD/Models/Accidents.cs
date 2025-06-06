namespace projektSBD.Models
{
    public class Accidents
    {
        public int AccidentID { get; set; }
        public int CarID { get; set; }
        public DateTime AccidentDate { get; set; }
        public string Location { get; set; }
        public string DamageDetails { get; set; }

        public Car Car { get; set; }
    }
}
