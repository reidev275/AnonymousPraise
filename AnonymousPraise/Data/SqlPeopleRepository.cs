using AnonymousPraise.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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

		public Person Get(string person)
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				var people = conn.Query<Person>(
					"Select Id, Name, Email from [dbo].[People] with (nolock) where name = @name",  
					param: new { name = person }
					);

				return people.FirstOrDefault();
			}
		}
	}
}