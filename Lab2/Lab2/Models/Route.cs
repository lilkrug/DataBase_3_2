namespace Lab2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Route")]
    public partial class Route
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string RouteName { get; set; }

        public decimal Distance { get; set; }

        [Required]
        [StringLength(100)]
        public string DeparturePoint { get; set; }

        [Required]
        [StringLength(100)]
        public string ArrivalPoint { get; set; }
    }
}
