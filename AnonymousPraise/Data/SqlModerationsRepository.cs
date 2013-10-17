using AnonymousPraise.Models;
using System.Data.SqlClient;
using System.Linq;

namespace AnonymousPraise.Data
{
	public class SqlModerationsRepository : IModerationsRepository
	{
		readonly string _connectionString;

		public SqlModerationsRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public void Insert(Moderation moderation)
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				conn.Execute("Insert into moderations (Guid, PraiseId) values(@Guid, @PraiseId)", new { Guid = moderation.Guid, PraiseId = moderation.PraiseId });
			}
		}

		public Moderation GetByGuid(string guid)
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				var results = conn.Query<Moderation>("Select Guid, PraiseId from moderations with (nolock) where Guid = @Guid", new { Guid = guid });
				return results.FirstOrDefault();
			}
		}

		public void Delete(int id)
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				conn.Execute("Delete from Praise where id = @id", new { id });
			}
		}

		public void MarkModerated(int id)
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				conn.Execute("Update Praise set Moderated = 1 where id = @id", new { id });
			}
		}
	}
}