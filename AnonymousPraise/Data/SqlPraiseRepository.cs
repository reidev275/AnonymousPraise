using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AnonymousPraise.Models;

namespace AnonymousPraise.Data
{
	public class SqlPraiseRepository : IPraiseRepository
	{
		private readonly string _connectionString;

		public SqlPraiseRepository(string connectionString)
		{
			if (connectionString == null) throw new ArgumentNullException("connectionString");
			_connectionString = connectionString;
		}

		public IEnumerable<Praise> GetAll()
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				return conn.Query<Praise>("spGetPraise",
						commandType: CommandType.StoredProcedure);
			}
		}

		public IEnumerable<Praise> GetByPerson(string person)
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				return conn.Query<Praise>("spGetPraise", 
						commandType: CommandType.StoredProcedure,
						param: new { Person = person });
			}
		}

		public IEnumerable<Praise> GetUnmoderated()
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				return conn.Query<Praise>("spGetPraise",
					commandType: CommandType.StoredProcedure,
					param: new { Moderated = 0 });
			}
		}

		public int Add(Praise praise)
		{
			using (var conn = new SqlConnection(_connectionString))
			using (var cmd = conn.CreateCommand())
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "spAddPraise";
				cmd.Parameters.AddWithValue("Person", praise.Person);
				cmd.Parameters.AddWithValue("Comment", praise.Comment);

				conn.Open();
				return Convert.ToInt32(cmd.ExecuteScalar());
			}
		}
		
		public void Like(int id)
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				conn.Execute("Insert into likes(PraiseId, Date) values(@id, getdate())", new { id });
			}
		}

		public Praise GetById(int id)
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				var results = conn.Query<Praise>("Select * from Praise with (nolock) where id = @id", new { id });
				return results.FirstOrDefault();
			}
		}
	}
}