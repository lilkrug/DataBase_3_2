using System;
using System.Collections.Generic;

#nullable disable

namespace Lab2.Models
{
    public partial class TrAudit
    {
        public int Id { get; set; }
        public string Stmt { get; set; }
        public string Trname { get; set; }
        public string Cc { get; set; }
    }
}
