#nullable disable
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainWatchSystem.Entities
{
    [Table("Rolling Stock")]

    public partial class RollingStock
    {
        [Key]

        [StringLength(24)]
        [Unicode(false)]
        [Required]
        public string ReportingMark { get; set; }
        [Required]
        [StringLength(50)]
        public string Owner { get; set; }
        public int Capacity { get; set; }
        public int? RailCarTypeID { get; set; }
        public int? YearBuilt { get; set;}
        [Required]
        public bool InService { get; set; }
        [StringLength(250)]
        public string Notes { get; set; }

        [ForeignKey("RailCarTypeID")]

        [InverseProperty("RollingStocks")]



        public virtual RailCarType RailCarType { get; set; }

        
    }
}