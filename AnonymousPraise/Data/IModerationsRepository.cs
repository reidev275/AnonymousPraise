using AnonymousPraise.Models;

namespace AnonymousPraise.Data
{
	public interface IModerationsRepository
	{
		void Insert(Moderation moderation);
		Moderation GetByGuid(string guid);
		void Delete(int id);
		void MarkModerated(int id);
	}
}
