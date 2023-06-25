using System;
using System.Collections.Generic;

#nullable disable

namespace Lab2.Models
{
    public partial class ServiceType
    {
        public ServiceType()
        {
        }

        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string UnitType { get; set; }

    }
}
