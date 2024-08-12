using Entities.Concrete;
using Shared.Data.Abstract;
using Shared.Entities.Abstract_Base_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccessLayer.EntityFramework.InterfaceRepositories
{
    public interface IProductRepository : IEntityRepository<Product>
    {
        //eklemek istediğimiz fonksiyonlar buraya
    }
}
