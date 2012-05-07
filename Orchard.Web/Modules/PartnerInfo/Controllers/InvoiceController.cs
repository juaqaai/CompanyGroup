﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Themes;

namespace Cms.PartnerInfo.Controllers
{
    [Themed]
    public class InvoiceController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}