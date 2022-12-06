#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainWatchSystem.Entities
{
    public partial class RollingStock
    {
        public string ReportingMark { get; set; }
        public string Owner { get; set; }
        public int Capacity { get; set; }
        public int RailCarTypeID { get; set; }
        public bool InService { get; set; }
        public virtual RailCarType RailCarType { get; set; }

        public virtual ICollection<RollingStock> RollingStocks { get; set; }
    }
}