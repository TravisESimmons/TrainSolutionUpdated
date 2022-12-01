
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainWatchSystem.Entities
{
    public partial class RollingStock
    {
        public RollingStock()
        {

            RollingStocks = new HashSet<RollingStock>();

        }

        [Key]
        [StringLength(24)]
        public string ReportingMark { get; set; }

        [Required]

        public string Owner { get; set; }
        [StringLength(50)]

        public int Capacity { get; set; }
        public int RailCarTypeID { get; set; }

        public bool InService { get; set; }


        public virtual ICollection<RollingStock> RollingStocks { get; set; }

        public virtual RailCarType RailCarType { get; set; }
    }
}