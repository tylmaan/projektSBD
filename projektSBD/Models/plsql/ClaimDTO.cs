namespace projektSBD.Models.plsql
{
    public class ClaimDto
    {
        public int CLAIMID { get; set; }
        public DateTime CLAIMDATE { get; set; }
        public string STATUS { get; set; }
        public decimal PAYOUTAMOUNT { get; set; }
    }
}
