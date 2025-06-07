using System.ComponentModel.DataAnnotations.Schema;

namespace projektSBD.Models
{
    [Table("OWNERS")]
    public class Owner
    {
        public int OWNERID { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string PHONENUMBER { get; set; }
        public string EMAIL { get; set; }

        public ICollection<CarOwner> CarOwners { get; set; }
    }
}
