using System.ComponentModel.DataAnnotations.Schema;

namespace projektSBD.Models
{
    [Table("ACCIDENTS")]
    public class Accidents
    {
        public int ACCIDENTID { get; set; }
        public int CARID { get; set; }
        public DateTime ACCIDENTDATE { get; set; }
        public string LOCATION { get; set; }
        public string DAMAGEDETAILS { get; set; }

        public Car Car { get; set; }
    }
}
