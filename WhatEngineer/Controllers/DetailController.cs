using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatEngineer.Controllers
{
    public class DetailController : Controller
    {
        [HttpGet]
        public IActionResult DetailInfo()
        {
            return View("work-single");
        }
    }
}
