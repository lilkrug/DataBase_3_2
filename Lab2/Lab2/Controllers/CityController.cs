using Lab2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Controllers
{
    public class CityController : Controller
    {
        public ActionResult GetAllCitys()
        {
            CityRepository CityRepo = new CityRepository();
            return View(CityRepo.GetAllCitys());
        }
        public ActionResult AddCity()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCity(City City)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CityRepository CityRepo = new CityRepository();

                    if (CityRepo.AddCity(City))
                    {
                        ViewBag.Message = "City details added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteCity(int id)
        {
            try
            {
                CityRepository CityRepo = new CityRepository();
                if (CityRepo.DeleteCity(id))
                {
                    ViewBag.AlertMsg = "City details deleted successfully";
                }
                return RedirectToAction("GetAllCitys");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditCity(int id)
        {
            CityRepository CityRepo = new CityRepository();

            return View(CityRepo.GetAllCitys().Find(City => City.Id == id));

        }

        [HttpPost]
        public ActionResult EditCity(City obj)
        {
            try
            {
                CityRepository CityRepo = new CityRepository();

                CityRepo.UpdateCity(obj);
                return RedirectToAction("GetAllCitys");
            }
            catch
            {
                return View();
            }
        }
    }
}