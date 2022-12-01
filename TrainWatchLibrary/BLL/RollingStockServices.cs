#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWatchSystem.DAL;
using TrainWatchSystem.Entities;

namespace TrainWatchSystem.BLL
{

    public class RollingStockServices
    {

        private readonly TrainWatchContext _context;


        internal RollingStockServices(TrainWatchContext regContext)
        {
            _context = regContext;
        }
        #region Services: Query



        public RollingStock RailCarType_GetByID(int RailCarTypeID)
        {


            RollingStock info = _context.RollingStocks
                                   .Where(x => x.RailCarTypeID == RailCarTypeID)
                                   .FirstOrDefault();
            return info;
        }

        public RollingStock RailCarType_GetByPartial(string SearchArg)
        {
            RollingStock info = (RollingStock)_context.RollingStocks.Where(item => item.ReportingMark.Contains(SearchArg));
                                                                            
            return info; 


        }

        public List<RailCarType> RailCarList()
        {


            IEnumerable<RailCarType> info = _context.RailCarTypes
                                   .OrderBy(x => x.Name);
            return info.ToList();

        }
    }
}
#endregion