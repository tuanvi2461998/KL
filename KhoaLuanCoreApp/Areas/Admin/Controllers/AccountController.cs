﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KhoaLuanCoreApp.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}