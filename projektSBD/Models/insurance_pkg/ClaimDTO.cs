using System.Text.Json.Serialization;

namespace projektSBD.Models.insurance_pkg
{
    public class ClaimDto
    {
        public int CLAIMID { get; set; }

        [JsonIgnore]
        public DateTime CLAIMDATE { get; set; }
        public string DATE => CLAIMDATE.ToString("yyyy-MM-dd");
        public string STATUS { get; set; }
        public decimal PAYOUTAMOUNT { get; set; }
    }
}
