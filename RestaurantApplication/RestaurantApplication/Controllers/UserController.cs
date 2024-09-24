using Microsoft.AspNetCore.Mvc;
using Restaurant_Model;
using Restaurant_Model.Request;
using Restaurant_Model.SearchObjects;
using Restaurant_Services;

namespace RestaurantApplication.Controllers
{
    public class UserController : BaseCRUDController<Restaurant_Model.Users, UserSearchObject, UserInsertRequest, UserUpdateRequest>
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;

        public UserController(IUserService userService, ITokenService tokenService) : base(userService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public ActionResult<Restaurant_Model.Users> Login([FromBody] LoginRequest loginRequest)
        {
            var user = userService.Login(loginRequest.Username, loginRequest.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = tokenService.GenerateToken(user);

            var response = new LoginResponse()
            {
                User = user,
                Token = token
            };

            return Ok(response);
        }
    }
}
