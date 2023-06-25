using System;
using System.Collections.Generic;

#nullable disable

namespace Lab2.Models
{
    public partial class Order
    {
        public Order()
        {
        }

        public Order(string customerName, int serviceId, decimal unitsAmount, DateTime orderDate, DateTime orderExec)
        {
            CustomerName = customerName;
            ServiceId = serviceId;
            UnitsAmount = unitsAmount;
            OrderDate = orderDate;
            OrderExec = orderExec;
        }

        public Order(int id, string customerName, int serviceId, decimal unitsAmount, DateTime orderDate, DateTime orderExec)
        {
            Id = id;
            CustomerName = customerName;
            ServiceId = serviceId;
            UnitsAmount = unitsAmount;
            OrderDate = orderDate;
            OrderExec = orderExec;
        }

        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int ServiceId { get; set; }
        public decimal UnitsAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderExec { get; set; }

    }
}
