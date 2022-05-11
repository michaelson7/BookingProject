using Application.interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.DataAccess
{
    public class DataAccess : IDataAccess
    {
        //this objects allows the system to access the database credentials
        private readonly IConfiguration _config;

        public DataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadDataAsync<T, U>(string sqlStatement,
                                      U parameters,
                                      string connectionStringName,
                                      bool isStoredProcedure = false)
        {
            try
            {
                string connectionString = _config.GetConnectionString(connectionStringName);
                CommandType commandType = CommandType.Text;

                if (isStoredProcedure == true)
                {
                    commandType = CommandType.StoredProcedure;
                }

                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    List<T> rows = new List<T>();
                    await Task.Run(() =>
                    {
                        rows = connection.Query<T>(sqlStatement, parameters, commandType: commandType).ToList();
                    });
                    return rows;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error on LoadData: {e.Message}");
                throw new ArgumentException($"EXCEPTION WHILE FETCHING DATA: {e.Message}");
            }
        }

        public async Task<int> SaveDataAsync<T>(string sqlStatement,
                                T parameters,
                                string connectionStringName,
                                bool isStoredProcedure = false)
        {
            try
            {
                string connectionString = _config.GetConnectionString(connectionStringName);
                CommandType commandType = CommandType.Text;

                if (isStoredProcedure == true)

                {

                    commandType = CommandType.StoredProcedure;
                }

                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    int primaryKey = new int();
                    await Task.Run(() =>
                    {
                        primaryKey = Convert.ToInt32(connection.ExecuteScalar(sqlStatement, parameters, commandType: commandType));

                    });
                    return primaryKey;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error on SaveData: {e.Message}");
                throw new ArgumentException($"EXCEPTION WHILE SaveData: {e.Message}");
            }
        }
    }
}
