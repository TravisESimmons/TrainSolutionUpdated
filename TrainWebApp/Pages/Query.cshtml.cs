using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using TrainWatchSystem.BLL;
using TrainWatchSystem.Entities;

namespace TrainWebApp.Pages
{
    public class QueryModel : PageModel
    {
        #region Private service fields & class constructor
        private readonly ILogger<IndexModel> _logger;
        private readonly RollingStockServices _rollingStockServices;
        private readonly TrainWatchServices _trainWatchServices;

        [BindProperty(SupportsGet = true)]
        public int RailCarTypeID { get; set; }


        [BindProperty]
        public List<RollingStock> RollingStocks { get; set; } = new List<RollingStock>();


        public QueryModel(ILogger<IndexModel> logger, RollingStockServices rollingStockServices, TrainWatchServices trainWatchServices)
        {
            _logger = logger;
            _rollingStockServices = rollingStockServices;
            _trainWatchServices = trainWatchServices;
        }
        #endregion

        [TempData]
        public string Feedback { get; set; }

        [BindProperty(SupportsGet = true)]
  
        public string ReportingMark { get; set; }

        [BindProperty]

        public string SearchArg { get; set; }

        [BindProperty]

        public List<RollingStock> RollingStockInfo { get; set; }

        [BindProperty]
        public List<RailCarType> RailCarInfo { get; set; }


        public void OnGet()
        {
            PopulateList();

            if (RailCarTypeID > 0)
            {
                RollingStock RollingStockInfo = _rollingStockServices.RailCarType_GetByID(RailCarTypeID);
                if (RollingStockInfo == null)
                {
                    Feedback = "RailCarTpe id is not valid.  No such id on file.";
                }
                else
                {
                    Feedback = $"ID: {RollingStockInfo.RailCarTypeID} Description: {RollingStockInfo.ReportingMark}";
                }
            }
        }
        private void PopulateList()
        {

            RailCarInfo = _trainWatchServices.RailCarTypes();
        }

        public IActionResult OnPostFetchRail()
        {
            if (string.IsNullOrWhiteSpace(SearchArg))
            {
                Feedback = "Required:  Search argument is empty";
            }
            return RedirectToPage(new { SearchArg , RailCarTypeID});
        }
    }
}
