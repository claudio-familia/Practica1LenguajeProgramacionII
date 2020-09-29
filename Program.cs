using LenguajeProgramacionII.Services;
using System.Threading.Tasks;

namespace LenguajeProgramacionII
{
    class Program
    {
        static async Task Main(string[] args)
        {
            MainService _mainService = new MainService();
            await _mainService.Run();
        }
    }
}
