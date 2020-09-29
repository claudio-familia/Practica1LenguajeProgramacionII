using LenguajeProgramacionII.Helpers;
using LenguajeProgramacionII.Models;
using LenguajeProgramacionII.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LenguajeProgramacionII.Services
{
    public class SupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly Util _helpers;     
        
        public SupplierService()
        {
            _supplierRepository = new SupplierRepository();
            _helpers = new Util();
        }

        public async Task CreateSupplier()
        {
            Console.WriteLine("Registro de suplidor \n \n");

            Supplier newSupplier = new Supplier
            {
                Name = _helpers.GetValue("Digite el nombre de la empresa"),
                Representant = _helpers.GetValue("Digite el nombre del representante de la empresa"),
                RNC = _helpers.GetValue("Digite el RNC de la empresa"),
                Address = _helpers.GetValue("Digite la dirección de la empresa"),
                Telephone = _helpers.GetValue("Digite el teléfono de la empresa"),
                IsProvider = "1" == _helpers.GetValue("Digite 1 si la empresa es suplidor de lo contrario escriba cualquier cosa"),              
            };

            if (newSupplier.IsProvider)
            {
                newSupplier.RPE = _helpers.GetValue("Digite el número de registro de proveedores del estado de la empresa");
            }

            var supplierFound = await _supplierRepository.Read(newSupplier.RNC);
            if (supplierFound.Id > 0)
            {
                Console.WriteLine("El RNC del suplidor que desea ingresar ya existe");
                return;
            }

            await _supplierRepository.Create(newSupplier);

            Console.WriteLine("Suplidor creado correctamente");
        }

        public async Task GetAllSuppliers()
        {
            string[] _tableColumns = { "RNC", "Nombre", "Representante", "Proveedor"};
            IEnumerable<Supplier> suppliers = await _supplierRepository.Read();

            Console.Clear();
            Console.WriteLine("Listado de suplidores registrados... \n \n");

            _helpers.PrintLine();
            _helpers.PrintRow(_tableColumns);
            _helpers.PrintLine();
            foreach (Supplier supplier in suppliers)
            {
                string[] _tableRow = { supplier.RNC, supplier.Name, supplier.Representant, supplier.IsProvider ? "Si" : "No" };
                _helpers.PrintRow(_tableRow);
            }
            _helpers.PrintLine();

            Console.WriteLine("\n Presione cualquier tecla para volver al menu...");
            Console.ReadKey();
        }

        public async Task UpdateSupplier(string rnc)
        {
            await GetSupplier(rnc);
            Console.WriteLine("Desea actualizar el registro seleccionado?");
            Console.WriteLine("1- Si");
            Console.WriteLine("0- No");

            string answer = Console.ReadLine();

            if(answer == "1")
            {
                if (await _supplierRepository.Exists(rnc))
                {
                    Supplier supplier = await _supplierRepository.Read(rnc);
                    supplier.Representant = _helpers.GetValue("Digite el nombre del representante de la empresa");
                    supplier.Address = _helpers.GetValue("Digite la dirección de la empresa");
                    supplier.Telephone = _helpers.GetValue("Digite el teléfono de la empresa");

                    await _supplierRepository.Update(supplier);
                    Console.WriteLine("Suplidor actualizado correctamente");
                }
            }
        }

        public async Task DeleteSupplier(string rnc)
        {
            await GetSupplier(rnc);
            Console.WriteLine("¿Esta seguro que desea eliminar el registro seleccionado?");
            Console.WriteLine("1- Si");
            Console.WriteLine("0- No");

            string answer = Console.ReadLine();

            if (answer == "1")
            {
                Supplier supplier = await _supplierRepository.Read(rnc);                

                await _supplierRepository.Delete(supplier.Id);

                Console.WriteLine("Suplidor eliminado correctamente");
            }
            else
            {
                Console.WriteLine("No se elimino el suplidor");
            }
        }

        public async Task GetSupplier(string rnc)
        {
            string[] _tableColumns = { "Datos", "Valor" };
            Supplier supplier = await _supplierRepository.Read(rnc);

            if(supplier.Id == 0)
            {
                Console.Clear();
                Console.WriteLine("No se encontro el suplidor solicitado \n \n");
                return;
            }

            Console.Clear();
            Console.WriteLine("Listado de suplidores registrados... \n \n");

            _helpers.PrintLine();
            _helpers.PrintRow(_tableColumns);
            _helpers.PrintLine();
            _helpers.PrintRow(new string[] {"RNC", supplier.RNC });
            _helpers.PrintRow(new string[] {"Nombre", supplier.Name });
            _helpers.PrintRow(new string[] {"Representante", supplier.Representant });
            _helpers.PrintRow(new string[] {"Direccion", supplier.Address });
            _helpers.PrintRow(new string[] {"Telefono", supplier.Telephone });
            _helpers.PrintRow(new string[] {"Proveedor", supplier.IsProvider ? "Si" : "No" });                                                
            _helpers.PrintRow(new string[] { "Número de RPE", supplier.RPE });
            _helpers.PrintLine();            
        }
    }
}
