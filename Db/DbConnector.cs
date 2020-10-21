using Dapper;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Threading;
using NilveraDemo.Exceptions;

namespace NilveraDemo.Db{

    public class DbConnector{
        
        private string connectionString = "";
        public DbConnector(string connString)
        {
            connectionString = connString;
        }

        public async Task Insert<T>(string sql, T[] data){

            using (var connection = new SqlConnection(connectionString))
            {
                try{
                    await connection.OpenAsync();
                    await connection.ExecuteAsync(sql,data);
                    await connection.CloseAsync();
                }catch(SqlException e){
                    throw new ResponseException(e.Message);
                }
            }
        }
    }
}