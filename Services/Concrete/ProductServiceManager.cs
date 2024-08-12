using AutoMapper;
using DAL_DataAccessLayer.Abstarct;
using Entities.Concrete;
using Entities.Dtos;
using Services.Abstract;
using Shared.Utilities_araçlar_.Concrete;
using Shared.Utilities_araçlar_.Results;
using Shared.Utilities_araçlar_.Results.Abstract_interfaces_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class ProductServiceManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductServiceManager(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
               
        }


        public async Task<IDataResult<Product>> Add(ProductAddDto productAddDto)
        {
            if (productAddDto == null)
            {
                return new DataResult<Product>(ResultStatus.Error,"Product Adddto is null" );
            }
            var product = _mapper.Map<Product>(productAddDto);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
            return new DataResult<Product>(ResultStatus.Success, "Product Successfully added",product);
        }

        public async Task<IResult> Delete(Guid productId)
        {
            var product = await _unitOfWork.Products.GetAsync(p=>p.Id == productId);
            if(product != null)
            {
                await _unitOfWork.Products.DeleteAsync(product);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, "Product Removed Successfully");
            }
            return new Result(ResultStatus.Error, "Product Could 'not found");
        }

        public async Task<IDataResult<Product>> Get(Guid productId)
        {
            var product = await _unitOfWork.Products.GetAsync(p=>p.Id ==productId);
            if(product != null)
            {
                return new DataResult<Product>(ResultStatus.Success, product);
            }
            return new DataResult<Product>(ResultStatus.Error, "Could not find he Product", null);
        }

        public async Task<IDataResult<IList<Product>>> GetAll()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            if(products.Count > -1)
            {
                return new DataResult<IList<Product>>(ResultStatus.Success, products);
            }
            return new DataResult<IList<Product>>(ResultStatus.Error, "Could not find products", null);
        }

        public async Task<IResult> Update(ProductUpdateDto productUpdateDto)
        {
            if (productUpdateDto == null)
            {
                return new Result(ResultStatus.Error, "ProductUpdateDto is null");
            }
            var product = await _unitOfWork.Products.GetByIdAsync(productUpdateDto.Id);
            if (product != null)
            {
                // Mevcut ürünü DTO'dan gelen verilerle güncelleme
                product.Name = productUpdateDto.Name;
                product.Description = productUpdateDto.Description;
                product.ImageUrls = productUpdateDto.ImageUrls;
                product.StockQuantity = productUpdateDto.StockQuantity;
                product.Price = productUpdateDto.Price;
                // Güncellenen ürünü veritabanına kaydetme
                await _unitOfWork.Products.UpdateAsync(product);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Success, "Product successfully updated.");
            }
            else
            {
                return new Result(ResultStatus.Error, "Product could not be found.");
            }

        }
    }
}
