using DAL_DataAccessLayer.EntityFramework.InterfaceRepositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccessLayer.EntityFramework.Repositories
{
    internal class CartRepository : EfEntityRepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(DbContext context) : base(context)
        {
        }
    }
}
