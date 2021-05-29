﻿using BL.DTO.Models;
using BL.Implementation.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly MVCUserService _service;

        public AccountController(MVCUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new UserRegisterModel()
            {
                AllRoles = _service.GetRoles().ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AllRoles = _service.GetRoles().ToList();
                return View(model);
            }

            var result = await _service.Register(model);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            model.AllRoles = _service.GetRoles().ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserLoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await _service.Login(model);

            if (res.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _service.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
    


