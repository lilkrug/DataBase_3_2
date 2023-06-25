using Lab2.DAL;
using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Controllers
{
    public class ServiceController : Controller
    {
        public ActionResult GetAllServices(string status)
        {
            ServiceRepository ServiceRepo = new ServiceRepository();
            IEnumerable<Service> services =  ServiceRepo.GetAllServices();
            ServiceTypeRepository serviceTypeRepository = new ServiceTypeRepository();
            IEnumerable<ServiceType> serviceTypes = serviceTypeRepository.GetAllServiceTypes();
            if(status !="Все" && status != null)
            {
                services = ServiceRepo.GetAllServices().Where(x => x.ServiceType == status).ToList();
            }
            List<string> serviceTypeList = serviceTypeRepository.GetAllServiceTypes().Select(x=>x.ServiceName).ToList();
            serviceTypeList.Add("Все");
            ServiceViewModel serviceViewModel = new ServiceViewModel
            {
                Services = services,
                Statuses = new SelectList(serviceTypeList)
            };
            return View(serviceViewModel);
        }
        public ActionResult AddService()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddService(Service Service)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ServiceRepository ServiceRepo = new ServiceRepository();

                    if (ServiceRepo.AddService(Service))
                    {
                        ViewBag.Message = "Service details added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteService(int Id)
        {
            try
            {
                ServiceRepository ServiceRepo = new ServiceRepository();
                if (ServiceRepo.DeleteService(Id))
                {
                    ViewBag.AlertMsg = "Service details deleted successfully";
                }
                return RedirectToAction("GetAllServices");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditService(int id)
        {
            ServiceRepository ServiceRepo = new ServiceRepository();

            return View(ServiceRepo.GetAllServices().Find(Service => Service.Id == id));

        }

        [HttpPost]
        public ActionResult EditService(Service obj)
        {
            try
            {
                ServiceRepository ServiceRepo = new ServiceRepository();

                ServiceRepo.UpdateService(obj);
                return RedirectToAction("GetAllServices");
            }
            catch
            {
                return View();
            }
        }
    }
}