#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWatchSystem.Entities;
using TrainWatchSystem.DAL;

namespace TrainWatchSystem.BLL
{
    public class TrainWatchServices
    {
        private readonly TrainWatchContext _context;

        internal TrainWatchServices(TrainWatchContext regContext)
        {
            _context = regContext;
        }
        #region Services: Query 
        #endregion

        public List<RailCarType> RailCarTypes()
        {

            IEnumerable<RailCarType> info = _context.RailCarTypes.OrderBy(item => item.Name);
            return info.ToList();

        }

        public DbVersion GetDbVersion()
        {

            DbVersion info = _context.DbVersions.FirstOrDefault();

            return info;
        }
    }
}
