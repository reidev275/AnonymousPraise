using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnonymousPraise.Data
{
	public class SqlPeopleRepository : IPeopleRepository
	{
		private readonly string _connectionString;

		public SqlPeopleRepository(string connectionString)
		{
			if (connectionString == null) throw new ArgumentNullException("connectionString");
			_connectionString = connectionString;
		}

		public IEnumerable<string> GetAll()
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				return conn.Query<string>(
					"Select Name from [dbo].[People] with (nolock) order by Name"
					);
			}
		}
	}
}