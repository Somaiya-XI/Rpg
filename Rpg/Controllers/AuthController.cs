using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rpg.Dtos.User;

namespace Rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var resp = await _authRepo.Register(
                new User { Username = request.Username },
                 request.Password);
            if (!resp.IsOk)
                return BadRequest(resp);
            return Ok(resp);

        }
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
        {
            var resp = await _authRepo.Login(request.Username, request.Password);
            if (!resp.IsOk)
                return BadRequest(resp);
            return Ok(resp);
        }
    }
}