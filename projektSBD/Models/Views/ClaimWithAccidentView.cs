using System.Text.Json.Serialization;

namespace projektSBD.Models.Views
{
    public class ClaimWithAccidentView
    {
        
        public int CLAIMID { get; set; }
        [JsonIgnore]
        public DateTime CLAIMDATE { get; set; }
        public string DATE =>
            CLAIMDATE.ToString("yyyy-MM-dd");
        public string STATUS { get; set; }
        public decimal PAYOUTAMOUNT { get; set; }
        public int POLICYID { get; set; }
        public int ACCIDENTID { get; set; }
        public int CARID { get; set; }
        [JsonIgnore]
        public DateTime ACCIDENTDATE { get; set; }
        public string DATEE =>
            ACCIDENTDATE.ToString("yyyy-MM-dd");
        public string LOCATION { get; set; }
        public string DAMAGEDETAILS { get; set; }
    }
}
