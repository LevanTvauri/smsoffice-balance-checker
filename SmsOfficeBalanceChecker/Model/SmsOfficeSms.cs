namespace SmsOfficeBalanceChecker.Model
{
    public class SmsOfficeSms
    {
        public int Id { get; set; }
        public string? Key { get; set; }
        public string? Destination { get; set; }
        public string? Sender { get; set; }
        public string? Content { get; set; }
        public bool Urgent { get; set; }
    }
}