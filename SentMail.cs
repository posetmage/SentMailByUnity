using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.ComponentModel;
using System.Net.Mail;

using System.Text;

using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class SentMail : MonoBehaviour
{
    //https://www.youtube.com/watch?v=lk5dhDzfzsU
    private SmtpClient mSmtpServer;

    // Start is called before the first frame update
    void Start()
    {

        // Command-line argument must be the SMTP host.
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        client.Credentials = new System.Net.NetworkCredential(
            "posetmage@gmail.com",
            "aaaaaaaaa");
        client.EnableSsl = true;
        // Specify the email sender.
        // Create a mailing address that includes a UTF8 character
        // in the display name.
        MailAddress from = new MailAddress(
            "youremail@gmail.com",
            "Your Name",
            System.Text.Encoding.UTF8);
        // Set destinations for the email message.
        MailAddress to = new MailAddress("posetmage@outlook.com");
        // Specify the message content.
        MailMessage message = new MailMessage(from, to);
        message.Body = "This is a test email message sent by an application. ";
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Subject = "test message 1";
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        // Set the method that is called back when the send operation ends.
        client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
        // The userState can be any object that allows your callback
        // method to identify this send operation.
        // For this example, the userToken is a string constant.
        string userState = "test message1";
        client.SendAsync(message, userState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        // Get the unique identifier for this asynchronous operation.
        string token = (string)e.UserState;

        if (e.Cancelled)
        {
            Debug.Log("Send canceled " + token);
        }
        if (e.Error != null)
        {
            Debug.Log("[ " + token + " ] " + " " + e.Error.ToString());
        }
        else
        {
            Debug.Log("Message sent.");
        }
    }
}
