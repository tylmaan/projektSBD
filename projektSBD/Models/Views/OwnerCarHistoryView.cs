using System.Text.Json.Serialization;

namespace projektSBD.Models.Views
{
    public class OwnerCarHistoryView
    {
        public int OWNERID { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public int CARID { get; set; }
        public string BRAND { get; set; }
        public string MODEL { get; set; }
        public int YEAR { get; set; }

        [JsonIgnore]
        public DateTime? SERVICEDATE { get; set; }
        public string DATE1 =>
            SERVICEDATE?.ToString("yyyy-MM-dd");

        public string SERVICE_DESCRIPTION { get; set; }
        public decimal? SERVICE_COST { get; set; }

        [JsonIgnore]
        public DateTime? ACCIDENTDATE { get; set; }
        public string DATE2 =>
            ACCIDENTDATE?.ToString("yyyy-MM-dd");

        public string LOCATION { get; set; }
        public string DAMAGEDETAILS { get; set; }

        [JsonIgnore]
        public DateTime? POLICYSTART { get; set; }
        public string DATE3 =>
            POLICYSTART?.ToString("yyyy-MM-dd");

        [JsonIgnore]
        public DateTime? POLICYEND { get; set; }
        public string DATE4 =>
            POLICYEND?.ToString("yyyy-MM-dd");

        public string POLICYSTATUS { get; set; }
    }
}
