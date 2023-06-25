using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Models
{
    public class ServiceViewModel
    {
        public IEnumerable<Service> Services { get; set; }
        public SelectList Statuses { get; set; }
    }
}