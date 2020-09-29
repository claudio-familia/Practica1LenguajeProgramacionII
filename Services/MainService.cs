using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LenguajeProgramacionII.Services
{    
    public class MainService
    {
        private readonly SupplierService _supplierService;
        private bool Exit { get; set; }        

        public MainService()
        {
            _supplierService = new SupplierService();            
        }        

        public async Task Run()
        {
            Exit = true;            

            while (Exit)
            {                
                Console.Clear();
                ShowMenu();
                Exit = await ChooseMenuOption(Console.ReadLine());
                await Task.Delay(1000);
            }
        }

        public void ShowMenu()
        {
            Console.WriteLine("Bienvenido al menu del sistema de suplidores... \n \n");
            Console.WriteLine("1- Registrar un suplidor");
            Console.WriteLine("2- Listado de suplidores");
            Console.WriteLine("3- Buscar un suplidor");
            Console.WriteLine("4- Actualizar un suplidor");
            Console.WriteLine("5- Eliminar un suplidor");
            Console.WriteLine("0- Salir del sistema");

            Console.WriteLine("\n Seleccione la opcion que desea realizar.");
        }

        public async Task<bool> ChooseMenuOption(string chosenOption)
        {
            switch (chosenOption)
            {
                case "1":
                    await _supplierService.CreateSupplier();
                    return true;                    

                case "2":
                    await _supplierService.GetAllSuppliers();
                    return true;                    

                case "3":
                    Console.WriteLine("\n Digite el rnc del suplidor que desea buscar...");
                    await _supplierService.GetSupplier(Console.ReadLine());
                    Console.WriteLine("\n Presione cualquier tecla para volver al menu...");
                    Console.ReadKey();
                    return true;                    

                case "4":
                    Console.WriteLine("\n Digite el rnc del suplidor que desea actualizar...");
                    await _supplierService.UpdateSupplier(Console.ReadLine());
                    Console.WriteLine("\n Presione cualquier tecla para volver al menu...");
                    Console.ReadKey();
                    return true;                    

                case "5":
                    Console.WriteLine("\n Digite el rnc del suplidor que desea eliminar...");
                    await _supplierService.DeleteSupplier(Console.ReadLine());
                    Console.WriteLine("\n Presione cualquier tecla para volver al menu...");
                    Console.ReadKey();
                    return true;                    

                case "0":
                    Console.WriteLine("Gracias, Vuelva pronto...");
                    return false;

                default:
                    Console.WriteLine("Has seleccionado una opción incorrecta...");
                    return true;                    
            }
        }      
    }
}
