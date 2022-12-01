using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using TrainWatchSystem.BLL;
using TrainWatchSystem.Entities;

namespace TrainWebApp.Pages
{
    public class TrainWatchQueryModel : PageModel
    {
        #region Private service fields & class constructor


        private readonly RollingStockServices _rollingStockServices;

        [BindProperty(SupportsGet = true)]
        public int RailCarTypeID { get; set; }

        //  The List<T> has a null value as the page is created
        //  You can initialize the property to an instance as the page is
        //      being created by adding = new List<Region>() or = new()
        //      to your declaration
        //  If you do, you will have an empty instance of List<T>
        [BindProperty]

        public List<RollingStock> RollingStocks{ get; set; } = new List<RollingStock>();

        [BindProperty]


    public int SelectRegion { get; set; }
        public TrainWatchQueryModel(RollingStockServices rollingStockServices)
        {

           
            _rollingStockServices = rollingStockServices;
        }
        #endregion

        [TempData]
        public string Feedback { get; set; }

        [BindProperty(SupportsGet = true)]

        public string SearchArg { get; set; }

        public List<RollingStock> RollingStockInfo { get; set; }

        [BindProperty]
        public List<RailCarType> RailCarInfo { get; set; }

        public void OnGet()
        {
            
        }
    }
}
