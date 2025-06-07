using System;

namespace projektSBD.Models
{
    public class Claim
    {
        public int CLAIMID { get; set; }
        public int ACCIDENTID { get; set; }
        public int POLICYID { get; set; }
        public DateTime CLAIMDATE { get; set; }
        public string STATUS { get; set; }
        public decimal PAYOUTAMOUNT { get; set; }

        public InsurancePolicy InsurancePolicy { get; set; }
        public Accidents Accident { get; set; }
    }
}
