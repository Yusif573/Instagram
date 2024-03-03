using System;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main(string[] args)
    {
        // Sender's email address and credentials
        string senderEmail = "yusif.veliyev573@gmail.com";
        string senderPassword = "lvrl hqhv sqve ncbm";

        // Recipient's email address
        string recipientEmail = "yusif.veliyev573@gmail.com";

        // Mail server and port
        string smtpServer = "smtp.gmail.com";
        int port = 587; // Port number depends on your mail server configuration (587 is a common port for SMTP submission)

        // Create a new SmtpClient instance
        SmtpClient client = new SmtpClient(smtpServer, port);
        client.EnableSsl = true; // Enable SSL/TLS encryption if required by your mail server

        // Set credentials (username/password) for the sender's email account
        client.Credentials = new NetworkCredential(senderEmail, senderPassword);

        // Create a new MailMessage instance
        MailMessage message = new MailMessage(senderEmail, recipientEmail);
        message.Subject = "Verifying from instagram";
        Random random = new Random();
        int randomNumber = random.Next(1000);
        message.Body = $"{randomNumber}";

        try
        {
            // Send the email
            client.Send(message);
            Console.WriteLine("Email sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
        finally
        {
            // Clean up resources
            message.Dispose();
            client.Dispose();
        }
    }
}