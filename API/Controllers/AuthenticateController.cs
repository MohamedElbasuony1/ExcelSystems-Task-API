using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public AuthenticateController(UserService userService,ILoggerManager logger,IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> PostLogin([FromBody]UserModel loginUser)
        {
            IActionResult response = BadRequest();
            User user =await _userService.Authenticate(loginUser);
            _logger.LogInfo("Before User Login");
            if (user != null)
            {
                _logger.LogInfo("User Is Authenticated");
                response = Ok(_userService.BuildToken(_mapper.Map<UserTokenModel>(user)));
            }
            return response;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> PostSignUp([FromBody]UserModel signUpUser)
        {
            IActionResult response = BadRequest();
            _logger.LogInfo("before User Sign Up");
            if (! await _userService.IsExistByUserName(signUpUser.UserName))
            {
                _logger.LogInfo("UserName is Unique");
                User user =await _userService.SignUp(_mapper.Map<User>(signUpUser));
                _logger.LogInfo(" User Sign Up Successfully");
                response = Ok(_userService.BuildToken(_mapper.Map<UserTokenModel>(user)));
            }
            return response;
        }

    }
}