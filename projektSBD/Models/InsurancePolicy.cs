using System;
using System.Collections.Generic;

namespace projektSBD.Models
{
    public class InsurancePolicy
    {
        public int POLICYID { get; set; }
        public int CARID { get; set; }
        public DateTime STARTDATE { get; set; }
        public DateTime ENDDATE { get; set; }
        public decimal COVERAGEAMOUNT { get; set; }
        public decimal PREMIUM { get; set; }
        public string POLICYSTATUS { get; set; }

        public Car Car { get; set; }
        public ICollection<Claim> Claims { get; set; }
    }
}
