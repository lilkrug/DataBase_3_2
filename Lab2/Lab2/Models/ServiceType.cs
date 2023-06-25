namespace Lab2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServiceType")]
    public partial class ServiceType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ServiceName { get; set; }
    }
}
