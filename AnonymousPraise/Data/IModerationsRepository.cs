using AnonymousPraise.Models;

namespace AnonymousPraise.Data
{
	public interface IModerationsRepository
	{
		void Insert(Moderation moderation);
		Moderation GetByGuid(string guid);
		void Decline(int id);
		void Approve(int id);
		void Delete(string guid);
	}
}
