using System.Text.Json.Serialization;

namespace projektSBD.Models.plsql
{
    public class CarAccident
    {
        [JsonIgnore]
        public DateTime ACCIDENTDATE { get; set; }

        public string DATE =>
            ACCIDENTDATE.ToString("yyyy-MM-dd");

        public string LOCATION { get; set; }
        public string DAMAGEDETAILS { get; set; }
    }
}
