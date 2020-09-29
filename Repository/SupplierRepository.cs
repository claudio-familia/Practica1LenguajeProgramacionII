using LenguajeProgramacionII.Models;
using LenguajeProgramacionII.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LenguajeProgramacionII.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly string connectionString;
        private readonly DatabaseService _databaseService;

        public SupplierRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["mainConnection"].ConnectionString;
            _databaseService = new DatabaseService();
        }

        public async Task<Supplier> Create(Supplier supplier)
        {
            string isProvider = supplier.IsProvider ? "1" : "0";
            string query = $"INSERT INTO Suppliers VALUES " +
                           $"('{supplier.Name}', '{supplier.Representant}', '{supplier.RNC}', '{supplier.Address}', " +
                           $"'{supplier.Telephone}', '{isProvider}', '{supplier.RPE}', '0')";

            await _databaseService.ExecuteQuery(connectionString, query);
            return supplier;
        }

        public async Task Delete(int id)
        {
            string query = $"UPDATE Suppliers SET Borrado = 1 WHERE Id = '{id}'";
            await _databaseService.ExecuteQuery(connectionString, query);
        }

        public async Task<IEnumerable<Supplier>> Read()
        {
            List<Supplier> suppliers = new List<Supplier>();
            DataTable reader = await _databaseService.ExecuteQueryReader(connectionString, "SELECT * FROM Suppliers WHERE Borrado = 0");

            for (int index = 0; index < reader.Rows.Count; index++)
            {
                Supplier supplier = new Supplier()
                {
                    Id = (int)reader.Rows[index].ItemArray[0],
                    Name = reader.Rows[index].ItemArray[1].ToString(),
                    Representant = reader.Rows[index].ItemArray[2].ToString(),
                    RNC = reader.Rows[index].ItemArray[3].ToString(),
                    Address = reader.Rows[index].ItemArray[4].ToString(),
                    Telephone = reader.Rows[index].ItemArray[5].ToString(),
                    IsProvider = (bool)reader.Rows[index].ItemArray[6],
                    RPE = reader.Rows[index].ItemArray[7].ToString()
                };

                suppliers.Add(supplier);
            }

            return suppliers;
        }

        public async Task<Supplier> Read(string rnc)
        {
            Supplier supplier = new Supplier();

            string query = $"SELECT * FROM Suppliers Where RNC = '{rnc}' AND Borrado = 0";

            DataTable reader = await _databaseService.ExecuteQueryReader(connectionString, query);

            for (int index = 0; index < reader.Rows.Count; index++)
            {
                supplier.Id = (int)reader.Rows[index].ItemArray[0];
                supplier.Name = reader.Rows[index].ItemArray[1].ToString();
                supplier.Representant = reader.Rows[index].ItemArray[2].ToString();
                supplier.RNC = reader.Rows[index].ItemArray[3].ToString();
                supplier.Address = reader.Rows[index].ItemArray[4].ToString();
                supplier.Telephone = reader.Rows[index].ItemArray[5].ToString();
                supplier.IsProvider = (bool)reader.Rows[index].ItemArray[6];
                supplier.RPE = reader.Rows[index].ItemArray[7].ToString();
            }

            return supplier;
        }

        public async Task<Supplier> Update(Supplier supplier)
        {
            string query = $"UPDATE Suppliers SET NombreEmpresa = '{supplier.Name}', " +
                                                $"PersonaRepresentante = '{supplier.Representant}', " +
                                                $"Direccion = '{supplier.Address}', " +
                                                $"Telefono = '{supplier.Telephone}' " +
                                                $"WHERE EmpresaId = {supplier.Id}";
            await _databaseService.ExecuteQuery(connectionString, query);
            return supplier;
        }

        public async Task<bool> Exists(string rnc)
        {
            string query = $"SELECT COUNT(*) FROM Suppliers Where RNC = {rnc} AND Borrado = 0";
            DataTable reader = await _databaseService.ExecuteQueryReader(connectionString, query);

            return reader.Rows.Count > 0;
        }
    }
}
