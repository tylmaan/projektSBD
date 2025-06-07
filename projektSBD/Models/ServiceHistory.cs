using System.ComponentModel.DataAnnotations.Schema;

namespace projektSBD.Models
{
    [Table("SERVICEHISTORY")]
    public class ServiceHistory
    {
        public int SERVICEID { get; set; }
        public int CARID { get; set; }
        public DateTime SERVICEDATE { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal COST { get; set; }

        public Car Car { get; set; }
    }
}
