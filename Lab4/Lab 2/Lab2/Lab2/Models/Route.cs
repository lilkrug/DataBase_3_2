using System;
using System.Collections.Generic;

#nullable disable

namespace Lab2.Models
{
    public partial class Route
    {
        public Route()
        {
        }

        public int Id { get; set; }
        public string RouteName { get; set; }
        public decimal Distance { get; set; }
        public string DeparturePoint { get; set; }
        public string ArrivalPoint { get; set; }

    }
}
