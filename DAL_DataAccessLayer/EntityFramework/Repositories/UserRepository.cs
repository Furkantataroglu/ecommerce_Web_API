using DAL_DataAccessLayer.EntityFramework.InterfaceRepositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Concrete.EntityFramework;
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
        public UserRepository(DbContext context) : base(context)
        {
        }

        public IList<User> GetUsersByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
