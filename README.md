
# smsoffice-balance-checker

[![.NET Core](https://img.shields.io/badge/.NET%20Core-5.0%2B-blue)](https://dotnet.microsoft.com/)

A simple .NET Core application that monitors the SMS balance on [SMSOffice.ge](https://smsoffice.ge) and sends low balance notifications to a pre-defined mobile number. The application uses `appsettings.json` for configuration.

## Features

- Checks SMS balance on SMSOffice.ge using the provided API key.
- Sends low balance notifications to a verified mobile number when the balance drops below a specified threshold.
- Configurable via `appsettings.json`.

---

## Getting Started

### Prerequisites

- .NET Core SDK version **5.0** or above.
- A valid account and API key from [SMSOffice.ge](https://smsoffice.ge).

---

### Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/LevanTvauri/smsoffice-balance-checker.git
   cd smsoffice-balance-checker
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Configure the application by editing the `appsettings.json` file. Replace the placeholders with actual values:
   ```json
   {
     "SmsOffice": {
       "ApiKey": "your-api-key",
       "SmsThreshold": 10000, 
       "ApiBaseUrl": "https://smsoffice.ge/api",
       "AlertNumber": "verified-mobile-number",
       "Sender": "your-sender-name"
     }
   }
   ```

   - **ApiKey**: Your SMSOffice.ge API key.
   - **SmsThreshold**: The balance threshold at which to trigger an alert.
   - **ApiBaseUrl**: Base URL for the SMSOffice.ge API.
   - **AlertNumber**: Mobile number to receive the low balance alert.
   - **Sender**: Sender name registered with SMSOffice.ge.

4. Build the project:
   ```bash
   dotnet build
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

---

## How It Works

1. The application queries the SMSOffice.ge API to check the current balance.
2. If the balance falls below the threshold specified in `appsettings.json`, an SMS notification is sent to the mobile number provided in the configuration.

---

## Configuration Example

Below is an example of the `appsettings.json` configuration:

```json
{
  "SmsOffice": {
    "ApiKey": "YOUR_API_KEY_HERE",
    "SmsThreshold": 10000,
    "ApiBaseUrl": "https://smsoffice.ge/api",
    "AlertNumber": "+995555123456",
    "Sender": "YourSenderName"
  }
}
```

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## Contact

For questions or feedback, feel free to reach out via the [repository issues](https://github.com/LevanTvauri/smsoffice-balance-checker/issues).
