namespace projektSBD.Models.plsql
{
    public class InsurancePolicyDto
    {
        public int POLICYID { get; set; }
        public decimal COVERAGEAMOUNT { get; set; }
        public decimal PREMIUM { get; set; }
        public DateTime STARTDATE { get; set; }
        public DateTime ENDDATE { get; set; }
        public string POLICYSTATUS { get; set; }
    }
}
