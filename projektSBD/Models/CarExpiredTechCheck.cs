using System.Text.Json.Serialization;

namespace projektSBD.Models
{
    public class CarExpiredTechCheck
    {
        public int CARID { get; set; }
        public string BRAND { get; set; }
        public string MODEL { get; set; }

        [JsonIgnore]
        public DateTime? LAST_SERVICE { get; set; }

        public string LAST_SERVICE_DATE =>
            LAST_SERVICE?.ToString("yyyy-MM-dd") ?? "Never";
    }
}
