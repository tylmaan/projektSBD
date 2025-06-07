using System.Text.Json.Serialization;

namespace projektSBD.Models.insurance_pkg
{
    public class InsurancePolicyDto
    {
        public int POLICYID { get; set; }

        public decimal COVERAGEAMOUNT { get; set; }
        public decimal PREMIUM { get; set; }

        [JsonIgnore]
        public DateTime STARTDATE { get; set; }

        public string DATE => STARTDATE.ToString("yyyy-MM-dd");

        [JsonIgnore]
        public DateTime ENDDATE { get; set; }

        public string DATEE => ENDDATE.ToString("yyyy-MM-dd");

        public string POLICYSTATUS { get; set; }
    }
}
