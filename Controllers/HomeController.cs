using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WorldData.Models;

namespace WorldData.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      City.GetCities();
      return View(Country.GetAll());
    }

    [HttpPost("/search/")]
    public ActionResult Search()
    {
      string searchId = Request.Form["searchId"];
      // Dictionary<string, Country> temp = Country.GetAll();
      // Dictionary<string, Country> fill = new Dictionary<string, Country> {};
      // foreach (KeyValuePair<string, Country> pair in temp)
      // {
      //   if (pair.Value.GetCode().Equals(searchId) || pair.Value.GetContinent().Equals(searchId) || pair.Value.GetName().Equals(searchId))
      //   {
      //     fill.Add(pair.Key, pair.Value);
      //   }
      // }
      return View("Index", Country.Find(searchId));
    }
  }
}
