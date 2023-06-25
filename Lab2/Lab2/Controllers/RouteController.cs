using Lab2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Controllers
{
    public class RouteController : Controller
    {
        public ActionResult GetAllRoutes()
        {
            RouteRepository RouteRepo = new RouteRepository();
            return View(RouteRepo.GetAllRoutes());
        }
        public ActionResult AddRoute()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRoute(Route Route)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RouteRepository RouteRepo = new RouteRepository();

                    if (RouteRepo.AddRoute(Route))
                    {
                        ViewBag.Message = "Route details added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteRoute(int Id)
        {
            try
            {
                RouteRepository RouteRepo = new RouteRepository();
                if (RouteRepo.DeleteRoute(Id))
                {
                    ViewBag.AlertMsg = "Route details deleted successfully";
                }
                return RedirectToAction("GetAllRoutes");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditRoute(int id)
        {
            RouteRepository RouteRepo = new RouteRepository();

            return View(RouteRepo.GetAllRoutes().Find(Route => Route.Id == id));

        }

        [HttpPost]
        public ActionResult EditRoute(Route obj)
        {
            try
            {
                RouteRepository RouteRepo = new RouteRepository();

                RouteRepo.UpdateRoute(obj);
                return RedirectToAction("GetAllRoutes");
            }
            catch
            {
                return View();
            }
        }
    }
}