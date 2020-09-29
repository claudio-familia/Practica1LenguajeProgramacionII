using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace LenguajeProgramacionII.Services
{
    public class DatabaseService
    {
        public async Task ExecuteQuery(string connectionString, string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = query;
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (Exception)
                    {
                        throw new Exception("Error al procesar");
                    }
                }
            }
        }

        public async Task<DataTable> ExecuteQueryReader(string connectionString, string query)
        {
            DataTable datareader = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = query;
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        datareader.Load(reader);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al procesar");
                    }
                }
            }

            return datareader;
        }
    }
}
