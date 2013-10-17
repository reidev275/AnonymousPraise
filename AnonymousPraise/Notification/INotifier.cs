using System.Net;
using System.Net.Mail;

namespace AnonymousPraise.Notification
{
	public interface INotifier
	{
		void Notify(string email, string message);
	}


}