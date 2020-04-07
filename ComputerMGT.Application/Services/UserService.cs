using ComputerMGT.Application.Interfaces;
using ComputerMGT.Application.ViewModels;
using ComputerMGT.Data.Interfaces;
using ComputerMGT.Domain.Models;
using ComputerMGT.Domain.Util;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<TblUser> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        #region Contructor
        /// <summary>
        /// Contructor UserService
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="userRepository"></param>
        public UserService(IUnitOfWork unitOfWork
            , IRepository<TblUser> userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        #endregion

        #region Create
        /// <summary>
        /// Create Client User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> CreateUser(CreateUserModel model)
        {
            if (!ValidateUtils.IsMail(model.Email)) throw new FormatException("Email address invalid");
            if (model.Password != model.Repassword) throw new FormatException("Wrong Repass");
            var query = _userRepository.GetManyAsNoTracking(x => x.Email.Equals(model.Email));
            if (query.ToList().Count > 0) throw new FormatException("Email is existed");
            var user = _userRepository.Insert(new TblUser
            {
                UserId = Guid.NewGuid(),
                Email = model.Email,
                Name = model.UserName,
                Password = model.Password,
                Phone = model.Phone,
                Role = false
            });
            await _unitOfWork.CommitAsync();
            return true;
        }
        #endregion

        #region Update User
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(UpdateUserModel model)
        {
            var query = _userRepository.GetById(model.UserId);
            if (query == null) throw new FormatException("User Not Found");
            if (!ValidateUtils.IsMail(model.Email)) throw new FormatException("Email address invalid");
            query.Name = model.UserName;
            query.Password = model.Password;
            query.Phone = model.Phone;
            query.Email = model.Email;
            _userRepository.Update(query);
            await _unitOfWork.CommitAsync();
            return true;
        }
        #endregion

        #region Login
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserDetailModel> Login(LoginModel model)
        {
            var query = _userRepository.GetManyAsNoTracking(x =>
                x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
            if (query == null) throw new Exception("Wrong Email Or Password");
            return new UserDetailModel
            {
                Email = query.Email,
                IsAdmin = query.Role,
                Name = query.Name,
                Phone = query.Phone,
                UserId = query.UserId
            };
        }
        #endregion
    }
}
