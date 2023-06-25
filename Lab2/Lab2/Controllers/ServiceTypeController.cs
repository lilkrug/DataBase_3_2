using Lab2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Controllers
{
    public class ServiceTypeController : Controller
    {
        public ActionResult GetAllServiceTypes()
        {
            ServiceTypeRepository ServiceTypeRepo = new ServiceTypeRepository();
            return View(ServiceTypeRepo.GetAllServiceTypes());
        }
        public ActionResult AddServiceType()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddServiceType(ServiceType ServiceType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ServiceTypeRepository ServiceTypeRepo = new ServiceTypeRepository();

                    if (ServiceTypeRepo.AddServiceType(ServiceType))
                    {
                        ViewBag.Message = "ServiceType details added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteServiceType(int Id)
        {
            try
            {
                ServiceTypeRepository ServiceTypeRepo = new ServiceTypeRepository();
                if (ServiceTypeRepo.DeleteServiceType(Id))
                {
                    ViewBag.AlertMsg = "ServiceType details deleted successfully";
                }
                return RedirectToAction("GetAllServiceTypes");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditServiceType(int id)
        {
            ServiceTypeRepository ServiceTypeRepo = new ServiceTypeRepository();

            return View(ServiceTypeRepo.GetAllServiceTypes().Find(ServiceType => ServiceType.Id == id));

        }

        [HttpPost]
        public ActionResult EditServiceType(ServiceType obj)
        {
            try
            {
                ServiceTypeRepository ServiceTypeRepo = new ServiceTypeRepository();

                ServiceTypeRepo.UpdateServiceType(obj);
                return RedirectToAction("GetAllServiceTypes");
            }
            catch
            {
                return View();
            }
        }

    }
}