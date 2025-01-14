namespace MyInsurance.Models.Domain
{
    public class Insurance
    {
        public Guid Id { get; set; }

        public string PolicyNumber { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string PolicyType { get; set; } = string.Empty;
        public decimal PremiumAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
