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
        public UserRepository(DbContext context, MyDbContext _context) : base(context)
        {
            this._context = _context;
        }

        public IList<User> GetUsersByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> UserLoginAsync(LoginDto loginDto)
        {
            var getUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (getUser != null)
            {
                return new Result(ResultStatus.Error, "Mail Or Password is wrong");
            }
            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, getUser.Password);
            if(checkPassword)
            {
                string token = GenerateJWTToken(getUser);
                return new Result(ResultStatus.Success,token);
            }
            else
            {
                return new Result(ResultStatus.Error, "Mail Or Password is Wrong")
            }
           
        }
    }
}
