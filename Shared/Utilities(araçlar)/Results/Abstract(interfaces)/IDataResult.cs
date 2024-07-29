using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities_araçlar_.Results.Abstract_interfaces_
{
    //kategori taşıyabilir user , hospital  falan out olmasınin sebebi mesela hastaneleri liste olarakta getirebiliri
    public interface IDataResult<out T>:IResult
    {
        public T Data { get; } //New DataResult<User>(ResultStatus.Success , user)
                               //new DataResult<IList<Category>>(ResultStatus.Success , userList); gibi
    }                           
}
