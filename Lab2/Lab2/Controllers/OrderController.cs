using Lab2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult GetAllOrders()
        {
            OrderRepository OrderRepo = new OrderRepository();
            return View(OrderRepo.GetAllOrders());
        }
        public ActionResult AddOrder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddOrder(Order Order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    OrderRepository OrderRepo = new OrderRepository();

                    if (OrderRepo.AddOrder(Order))
                    {
                        ViewBag.Message = "Order details added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteOrder(int Id,int ServiceId)
        {
            try
            {
                OrderRepository OrderRepo = new OrderRepository();
                if (OrderRepo.DeleteOrder(Id,ServiceId))
                {
                    ViewBag.AlertMsg = "Order details deleted successfully";
                }
                return RedirectToAction("GetAllOrders");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditOrder(int id)
        {
            OrderRepository OrderRepo = new OrderRepository();

            return View(OrderRepo.GetAllOrders().Find(Order => Order.Id == id));

        }

        [HttpPost]
        public ActionResult EditOrder(Order obj)
        {
            try
            {
                OrderRepository OrderRepo = new OrderRepository();

                OrderRepo.UpdateOrder(obj);
                return RedirectToAction("GetAllOrders");
            }
            catch
            {
                return View();
            }
        }

    }
}