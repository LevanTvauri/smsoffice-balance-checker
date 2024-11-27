namespace SmsOfficeBalanceChecker.Model;

public class SmsOfficeSmsResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string Output { get; set; }
    public int ErrorCode { get; set; }
}