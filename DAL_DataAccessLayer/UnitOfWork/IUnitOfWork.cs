using DAL_DataAccessLayer.EntityFramework.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccessLayer.Abstarct
{
    //repositorylerimizi tek bir yerde yönetmemizi sağlar .databaseye kaydetme işlemlerini kolaylaştırıyor ve her seferinde New Oluşturmamıza gerek kalmıyor.
    public interface IUnitOfWork:IAsyncDisposable
    {
        IUserRepository Users { get; } //unitofwork.Users gibi bir çağırım. 
        IProductRepository Products { get; }
        ICartRepository Carts { get; }
        ICartItemRepository CartItem { get; }
        //IRoleRepository Roles { get; }
        //IHospitalRepository Hospitals {get;}  gibi
        Task<int> SaveAsync();
        
    }
}
