using AuthorizationMicroservice.Models;
using AuthorizationMicroservice.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]

    public class AuthController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthController));
        private readonly IUserRepo repo;
        public AuthController(IUserRepo _repo)
        {
            repo = _repo;
        }
        [HttpPost]

        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            var user = repo.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = repo.GenerateJSONWebToken(user);

                response = Ok(new { token = tokenString, userId = login.EmployeeId.ToString() });
                _log4net.Info("In Authcontroller HttpPost Login method is initiated");
            }

            return response;
        }
    }
}
