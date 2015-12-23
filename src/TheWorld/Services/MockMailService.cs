using System.Diagnostics;
using TheWorld.Services;

public class MockMailService : IMailService
{
    public bool SendMail(string to, string from, string subject, string body)
    {
        Debug.WriteLine($"Sending mail: To: {to}, Subject: {subject}");
        return true;
    }
}
