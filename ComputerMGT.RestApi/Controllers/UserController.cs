using ComputerMGT.Application.Interfaces;
using ComputerMGT.Application.ViewModels;
using ComputerMGT.RestApi.ApiResponse;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ComputerMGT.RestApi.Controllers
{
    [ApiController]
    [Route("/api/users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Create the user
        /// <summary>Creates the user.</summary>
        /// <param name="model">The model.</param>
        [HttpPost]
        [Route("/api/users")]
        public async Task CreateUser([FromBody] CreateUserModel model)
        {
            await _userService.CreateUser(model);
            Response.StatusCode = (int)HttpStatusCode.OK;
        }
        #endregion

        #region Update User        
        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPut]
        [Route("/api/users")]
        public async Task UpdateUser([FromBody] UpdateUserModel model)
        {
            await _userService.UpdateUser(model);
            Response.StatusCode = (int)HttpStatusCode.OK;
        }
        #endregion

        #region Login        
        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ResponseModel&lt;UserDetailModel&gt;.</returns>
        [HttpPut]
        [Route("/api/users/login")]
        public async Task<ResponseModel<UserDetailModel>> Login([FromBody] LoginModel model)
        {
            var result = await _userService.Login(model);
            return new ResponseBuilder<UserDetailModel>().Success()
                .Data(result)
                .Count(1)
                .build();
        }
        #endregion
    }
}
