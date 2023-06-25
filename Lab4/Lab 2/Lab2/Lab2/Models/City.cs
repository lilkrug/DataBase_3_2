using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;

#nullable disable

namespace Lab2.Models
{
    public partial class City
    {
        public City()
        {

        }

        public int Id { get; set; }
        public string CityName { get; set; }
        public SqlGeography Point { get; set; }

    }
}
