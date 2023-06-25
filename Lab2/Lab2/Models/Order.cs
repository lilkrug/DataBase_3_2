namespace Lab2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string CustomerName { get; set; }

        public int ServiceId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime OrderExec { get; set; }

        public virtual Service Service { get; set; }
    }
}
