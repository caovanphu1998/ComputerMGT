using ComputerMGT.Application.Interfaces;
using ComputerMGT.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ComputerMGT.RestApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        #region Constructor
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion
        [HttpPost]
        [Route("users")]
        public async Task CreateUser([FromBody] CreateUserModel model)
        {
            await _userService.CreateUser(model);
            Response.StatusCode = (int)HttpStatusCode.OK;
        }
    }
}
