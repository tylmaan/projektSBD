using System.ComponentModel.DataAnnotations.Schema;

namespace projektSBD.Models
{
    [Table("CAROWNERS")]
    public class CarOwner
    {
        public int CARID { get; set; }
        public int OWNERID { get; set; }

        public Car Car { get; set; }

        public Owner Owner { get; set; }
    }
}
