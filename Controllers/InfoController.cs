using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WorldData.Models;

namespace WorldData.Controllers
{
  public class InfoController : Controller
  {
    [HttpGet("/{code}/info")]
    public ActionResult Info(string code)
    {
      return View(Country.GetAll()[code]);
    }
  }
}
