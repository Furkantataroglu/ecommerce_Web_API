using DAL_DataAccessLayer.EntityFramework.Contexts;
using DAL_DataAccessLayer.EntityFramework.InterfaceRepositories;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Concrete.EntityFramework;
using Shared.Utilities_araçlar_.Concrete;
using Shared.Utilities_araçlar_.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccessLayer.EntityFramework.Repositories
{
    public class UserRepository : EfEntityRepositoryBase<User>, IUserRepository  //bu sayede tekrarlı koddan kurtulmuş oluyoruz
    {
        private readonly MyDbContext _context;

        public UserRepository(DbContext context) : base(context)
        {
        }

        //public UserRepository(DbContext context) : base(context)
        //{
        //    this._context = context;
        //}

        public IList<User> GetUsersByName(string name)
        {
            throw new NotImplementedException();
        }

       
           
       
    }
}
