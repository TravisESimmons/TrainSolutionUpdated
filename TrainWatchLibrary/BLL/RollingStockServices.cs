#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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


        public List <RollingStock> RollingStocks()

        { 
            IEnumerable <RollingStock> info = _context.RollingStocks.OrderBy(x => x.ReportingMark);
            return info.ToList();

        }


        public List <RollingStock> GetByPartial(string SearchArg, int pageNumber,
                                                        int pageSize,
                                                        out int totalCount)
        {
            IEnumerable<RollingStock> info = _context.RollingStocks
                .Where(x => x.ReportingMark.Contains(SearchArg))
                .OrderBy(x => x.ReportingMark);
               
       

            totalCount = info.Count();
            //  Determine the number of rows to skip
            //  THis skipped count reflects the rows of the previous pages
            //  Remember the pagenumber is a natural number (1,2,3,....)
            //  This needs to be treated as an index (natural number -1)  Zero base
            //  The number of rows to skip is index * pagesize
            int skipRows = (pageNumber - 1) * pageSize;
            //  Return only the required number of rows.
            //  This will be done using filters belonging to LINQ
            //  Use the filter .Skip(n) to skip over n rows from the beginning of a collection
            //  Use the filter .Take(n) to take the next n rows from a collection
            return info.Skip(skipRows).Take(pageSize).ToList();

            //  This is the return statement that would be used IF no paging is being implemented
        }
       
        
    }
}
#endregion