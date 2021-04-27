﻿using BL.Abstraction;
using BL.DTO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <responce code="200">Success</responce>
        /// <responce code="400">Login failed.</responce>
        [HttpPost("Sign In")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            try
            {
                var token = await _userService.Login(model);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Register.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <responce code="200">Success</responce>
        [HttpPost("Sign Up")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var token = await _userService.Register(model);

            return Ok(token);
        }
    }
}
