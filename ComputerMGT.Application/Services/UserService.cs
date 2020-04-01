using ComputerMGT.Application.Interfaces;
using ComputerMGT.Application.ViewModels;
using ComputerMGT.Data.Interfaces;
using ComputerMGT.Domain.Models;
using ComputerMGT.Domain.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<TblUser> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        #region Contructor
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
            if (query.ToList().Count == 0) throw new FormatException("Email is existed");
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

    }
}
