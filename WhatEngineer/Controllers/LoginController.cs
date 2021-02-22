using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatEngineer.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult SignOutPage()
        {
            return View("SignOut");
        }
        [HttpGet]
        public IActionResult SignInPage()
        {
            return View("SignIn");
        }
    }
}
