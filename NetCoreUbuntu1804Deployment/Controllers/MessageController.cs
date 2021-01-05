using System.Data;
using System.Net;
using System.Net.Http;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Renci.SshNet.Messages;

namespace NetCoreUbuntu1804Deployment.Controllers
{
	[Route("/Message")]
	public class MessageController : ControllerBase
	{
		private const string ConnectionString = "Server=localhost;Database=TestDb;Uid=TestDbAdmin;Pwd=password;";

		[HttpGet]
		public string Get()
		{
			using ( var connection = CreateConnection() )
			{
				const string sql = "select Value from Message limit 1";
				return connection.QueryFirst<string>( sql );
			}
		}

		[HttpPost]
		public HttpResponseMessage Post(string message)
		{
			using ( var connection = CreateConnection() )
			{
				const string sql = "update Message set Value = @Message";
				connection.Execute( sql, new { Message = message } );
			}

			return new HttpResponseMessage( HttpStatusCode.OK );
		}

		private IDbConnection CreateConnection()
		{
			return new MySqlConnection( ConnectionString );
		}
	}
}