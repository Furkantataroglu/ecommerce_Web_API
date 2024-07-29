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
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class UserServiceManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(UserAddDto userAddDto)
        {
            var user = _mapper.Map<User>(userAddDto);
            user.CreatedDate = DateTime.Now;
            user.UpdatedBy = "Test admin";

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, "User Added");
        }

        public async Task<IResult> Update(UserUpdateDto userUpdateDto)
        {
            var user = await _unitOfWork.Users.GetAsync(c => c.Id == userUpdateDto.Id);
            if (user != null)
            {
                _mapper.Map(userUpdateDto, user);
                user.ModifiedDate = DateTime.Now;

                await _unitOfWork.Users.UpdateAsync(user);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, "User Updated Successfully");
            }
            return new Result(ResultStatus.Error, "User could not be updated");
        }

        public async Task<IResult> Delete(int userId)
        {
            var user = await _unitOfWork.Users.GetAsync(p => p.Id == userId);
            if (user != null)
            {
                await _unitOfWork.Users.DeleteAsync(user);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, "User Removed Successfully");
            }
            return new Result(ResultStatus.Error, "User could not be removed");
        }

        public async Task<IDataResult<User>> Get(int userId)
        {
            var user = await _unitOfWork.Users.GetAsync(c => c.Id == userId);
            if (user != null)
            {
                return new DataResult<User>(ResultStatus.Success, user);
            }
            return new DataResult<User>(ResultStatus.Error, "Could not find user", null);
        }

        public async Task<IDataResult<IList<User>>> GetAll()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            if (users.Count > -1)
            {
                return new DataResult<IList<User>>(ResultStatus.Success, users);
            }
            return new DataResult<IList<User>>(ResultStatus.Error, "Could not find users", null);
        }
    }
}
//BURASI AUTOMAPPERSİZ KISMI
//namespace Services.Concrete
//{
//    //BURAYI AUTO MAPPER İLE DE YAPABİLİRİZ İLERİDE VAR
//    public class UserServiceManager : IUserService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        public UserServiceManager(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<IResult> Add(UserAddDto userAddDto)
//        {
//            await _unitOfWork.Users.AddAsync(new User
//            {
//                FirstName = userAddDto.Name,
//                LastName = userAddDto.Surname,
//                CreatedDate = DateTime.Now,
//                UpdatedBy ="Test admin"


//            });
//            await _unitOfWork.SaveAsync();
//            return new Result(ResultStatus.Success, "User Added");
//        }
//        public async Task<IResult> Update(UserUpdateDto userUpdateDto)
//        {
//            var user = await _unitOfWork.Users.GetAsync(c => c.Id == userUpdateDto.Id);
//            if (user != null)
//            {
//                user.FirstName = userUpdateDto.Name;
//                user.LastName = userUpdateDto.Surname;
//                user.ModifiedDate = DateTime.Now;
//                await _unitOfWork.Users.UpdateAsync(user);
//                await _unitOfWork.SaveAsync();
//                return new Result(ResultStatus.Success, "User Updated Succesfully");
//            }
//            return new Result(ResultStatus.Error, "User could not updated");
//        }


//        public async Task<IResult> Delete(int userId)
//        {
//           var user = await _unitOfWork.Users.GetAsync(p=>p.Id == userId);
//            if(user != null)
//            {
//                await _unitOfWork.Users.DeleteAsync(user);  
//                await _unitOfWork.SaveAsync();
//                return new Result(ResultStatus.Success, "User Removed Succesfully");
//            }
//            return new Result(ResultStatus.Error, "User could not removed");
//        }

//        public async Task<IDataResult<User>> Get(int userId)
//        {
//           var user = await _unitOfWork.Users.GetAsync(c=> c.Id == userId); 
//            if(user != null)
//            {
//                return new DataResult<User>(ResultStatus.Success,user);
//            }
//            //if in içine girmezse
//            return new DataResult<User>(ResultStatus.Error,"Could not find user",null);
//        }

//        public async Task<IDataResult<IList<User>>> GetAll()
//        {
//           var users = await _unitOfWork.Users.GetAllAsync();
//            if(users.Count > -1)
//            {
//                return new DataResult<IList<User>>(ResultStatus.Success, users);
//            }
//            return new DataResult<IList<User>>(ResultStatus.Error, "Could not find users",null);
//        }


//    }
//}
