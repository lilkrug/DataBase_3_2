using System;
using System.Collections.Generic;

#nullable disable

namespace Lab2.Models
{
    public partial class Service
    {
        public Service()
        {
        }

        public int Id { get; set; }
        public string ServiceType { get; set; }
        public string RouteName { get; set; }
        public decimal CostPerUnit { get; set; }

    }
}
