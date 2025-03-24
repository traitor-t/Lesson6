public delegate void NotificationHandler(string message);
public class EmailNotifier
{
    public void SendEmail(string message)
    {
        Console.WriteLine($"Отправка уведомления по электронной почте: {message}");
    }
}

public class SmsNotifier
{
    public void SendSms(string message)
    {
        Console.WriteLine($"Отправка SMS уведомления: {message}");
    }
}
class Program
{
    static void Main(string[] args)
    {
        EmailNotifier emailNotifier = new EmailNotifier();
        SmsNotifier smsNotifier = new SmsNotifier();

        NotificationHandler handler;
        handler = emailNotifier.SendEmail;
        handler += smsNotifier.SendSms;
        handler("Это текстовое уведомление!");
    }
}