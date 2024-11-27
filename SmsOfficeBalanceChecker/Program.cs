using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SmsOfficeBalanceChecker.Model;

class Program
{
    private static async Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        IConfiguration config = builder.Build();
        
        await CheckSmsBalance(config);
    }
    private static async Task CheckSmsBalance(IConfiguration config)
    {
        var apiKey = config["SmsOffice:ApiKey"];
        var smsThreshold = int.Parse(config["SmsOffice:SmsThreshold"] ?? string.Empty);
        var url = $"{config["SmsOffice:ApiBaseUrl"]}/getBalance/?key={apiKey}";;
        using var client = new HttpClient();
        try
        {
            var response = await client.GetStringAsync(url);
            if (int.TryParse(response, out var balance))
            {
                if (balance < smsThreshold)
                {
                    await SendLowSmsAlterBySmsOffice(config, balance);
                }
            }
            else
            {
                Console.WriteLine("Failed to parse balance.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching balance: {ex.Message}");
        }

    }
    private static async Task SendLowSmsAlterBySmsOffice(IConfiguration config, int balance)
    {
        var sms = new SmsOfficeSms()
        {
            Content = $"Low SMS Balance - {balance}",
            Sender = config["SmsOffice:Sender"],
            Urgent = true,
            Destination = config["SmsOffice:AlertNumber"],
            Key = config["SmsOffice:ApiKey"]
        };
        try
        {
            if (String.IsNullOrEmpty(sms.Key) || String.IsNullOrEmpty(sms.Destination) || String.IsNullOrEmpty(sms.Sender))
            {
                Console.WriteLine("SMS alert not sent. Missing required fields.");
                return;
            }
            
            using var client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                { "key", sms.Key },
                { "destination",sms.Destination },
                { "sender", sms.Sender },
                { "content", sms.Content },
                { "urgent", "true" }
            };
            var content = new FormUrlEncodedContent(values!);
            var response = await client.PostAsync($"{config["SmsOffice:ApiBaseUrl"]}/v2/send/", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SmsOfficeSmsResponse>(jsonResult);
            if (result != null && !result.Success)
            {
                Console.WriteLine($"Error sending SMS alert: {result.Message}");
                return;
            }
            Console.WriteLine("Low SMS alert sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending SMS alert: {ex.Message}");
        }
    }
}
