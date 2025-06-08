using System.Text.Json.Serialization;

namespace projektSBD.Models.plsql
{
    public class CarRepair
    {
        [JsonIgnore]
        public DateTime SERVICEDATE { get; set; }
        public string DATE =>
            SERVICEDATE.ToString("yyyy-MM-dd");

        public string DESCRIPTION { get; set; }
        public decimal COST { get; set; }
    }
}
