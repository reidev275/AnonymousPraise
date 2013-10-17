using System;
using System.Net;
using System.Net.Mail;

namespace AnonymousPraise.Notification
{
	public class EmailNotifier : INotifier
	{
		readonly string _from;
		readonly string _password;

		public EmailNotifier(string from, string password)
		{
			_from = from;
			_password = password;
		}

		public void Notify(string email, string message)
		{
			if (String.IsNullOrWhiteSpace(email)) return;

			using (var mail = new MailMessage(_from, email))
			using (var smtp = new SmtpClient("smtp.gmail.com", 587) { Credentials = new NetworkCredential(_from, _password), EnableSsl = true })
			{
				mail.IsBodyHtml = true;
				mail.Body = message;
				mail.Subject = "Someone just gave you praise";
				smtp.Send(mail);
			}
		}
	}
}