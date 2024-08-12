using AutoMapper;
using DAL_DataAccessLayer.Abstarct;
using Entities.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using Shared.Entities.Token;
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
        private readonly UserManager<User> _userManager;

        private readonly ITokenService _tokenService;

        public UserServiceManager(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper,ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;

        }

        public async Task<IDataResult<NewUserDto>> Add(UserAddDto userAddDto)
        {
          
            // DTO'dan kullanıcı nesnesine dönüştür
            var user = new User()
            {
                FirstName = userAddDto.FirstName,
                LastName = userAddDto.LastName,
                Email = userAddDto.Email,
                PasswordHash = userAddDto.Password,
                UserName = userAddDto.Email,
            };
          
            // Kullanıcı oluşturma işlemini yap
            try
            {
                var result = await _userManager.CreateAsync(user,userAddDto.Password);
                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                    {
                        await _unitOfWork.SaveAsync();

                        return new DataResult<NewUserDto>(ResultStatus.Success,"User Added" ,new NewUserDto
                        {
                            Email = userAddDto.Email,
                            Token = _tokenService.CreateToken(user)
                        });
                    }
                    else
                    {
                        return new DataResult<NewUserDto>(ResultStatus.Error, "Role assignment failed: " + string.Join(", ", roleResult.Errors.Select(e => e.Description)), new NewUserDto());
                    }
                }
                else
                {
                    return new DataResult<NewUserDto>(ResultStatus.Error, "User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)), new NewUserDto());
                }
            }
            catch (Exception ex)
            {
                return new DataResult<NewUserDto>(ResultStatus.Error, "An error occurred: " + ex.Message, new NewUserDto());
            }
        }

        public async Task<IResult> Update(UserUpdateDto userUpdateDto)
        {
            // Kullanıcıyı veritabanından alın
            var user = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
            if (user == null)
            {
                return new Result(ResultStatus.Error, "User not found");
            }

            // Kullanıcıyı DTO ile güncelle
            user.FirstName = userUpdateDto.FirstName;
            user.LastName = userUpdateDto.LastName;
            user.Email = userUpdateDto.Email;
            user.UserName = userUpdateDto.Email;
            user.ModifiedDate = DateTime.Now;

            // Güncelleme işlemini yap
            try
            {
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _unitOfWork.SaveAsync();
                    return new Result(ResultStatus.Success, "User Updated Successfully");
                }
                else
                {
                    return new Result(ResultStatus.Error, "User update failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda genel bir hata mesajı döndür
                return new Result(ResultStatus.Error, "An error occurred: " + ex.Message);
            }
        }

        public async Task<IResult> Delete(Guid userId)
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

        public async Task<IDataResult<User>> Get(Guid userId)
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
