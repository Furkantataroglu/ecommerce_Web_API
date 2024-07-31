using Entities.Concrete;
using Entities.Dtos;
using Shared.Utilities_araçlar_.Results;
using Shared.Utilities_araçlar_.Results.Abstract_interfaces_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<User>> Get(int userId);
        Task<IDataResult<IList<User>>> GetAll();//tüm userleri getirecek
        Task<IResult> Add(UserAddDto userAddDto); // Kullanıcı ekleme
        Task<IResult> Update(UserUpdateDto userUpdateDto); // Kullanıcı güncelleme
        Task<IResult> Delete(int userId); // Kullanıcı silm

                

    }
}
