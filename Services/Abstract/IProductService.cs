using Entities.Concrete;
using Entities.Dtos;
using Shared.Entities.Token;
using Shared.Utilities_araçlar_.Results;
using Shared.Utilities_araçlar_.Results.Abstract_interfaces_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IProductService
    {
        Task<IDataResult<Product>> Get(Guid productId);
        Task<IDataResult<IList<Product>>> GetAll();
        Task<IDataResult<Product>> Add(ProductAddDto productAddDto); 
        Task<IResult> Update(ProductUpdateDto productUpdateDto); 
        Task<IResult> Delete(Guid productId);
        public int CalculateStockValue(Product product);
    }
}
