using LenguajeProgramacionII.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LenguajeProgramacionII.Repository
{
    public interface ISupplierRepository
    {
        public Task<Supplier> Create(Supplier supplier);
        public Task<Supplier> Update(Supplier supplier);
        public Task Delete(int id);
        public Task<IEnumerable<Supplier>> Read();
        public Task<Supplier> Read(string supplier);
        public Task<bool> Exists(string rnc);
    }
}
