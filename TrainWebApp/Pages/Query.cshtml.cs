#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using System.Drawing.Printing;
using TrainWatchSystem.BLL;
using TrainWatchSystem.Entities;
using TrainWebApp.Helpers;

namespace TrainWebApp.Pages
{
    public class QueryModel : PageModel
    {
        #region Private service fields & class constructor
        private readonly ILogger<IndexModel> _logger;
        private readonly RollingStockServices _rollingStockServices;
        private readonly TrainWatchServices _trainWatchServices;

        public QueryModel (ILogger<IndexModel> logger, RollingStockServices rollingStockServices, TrainWatchServices trainWatchServices)
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

        [BindProperty(SupportsGet = true)]

        public string SearchArg { get; set; }

        [BindProperty]

        public int searchid { get; set; }

        [BindProperty]

        public List <RollingStock> RollingStockInfo { get; set; }

        [BindProperty]
        public List <RailCarType> RailCarInfo { get; set; }

        [BindProperty(SupportsGet = true)]
        public int RailCarTypeID { get; set; }


        #region Paginator
        //  Desired page size
        private const int PAGE_SIZE = 25;
        //  Hold an instance of the Paginator
        public Paginator Pager { get; set; }
        #endregion


        public void OnGet(int? currentPage)
        {
            

            if (!string.IsNullOrWhiteSpace(SearchArg))
            {
                //  Setting up for using the Paginator only needs to be done if 
                //      a query is executing

                //  determine the current page number
                int pageNumber = currentPage.HasValue ? currentPage.Value : 1;

                //  Setup the current state of the pagomatpr (sizing)
                PageState current = new(pageNumber, PAGE_SIZE);

                //  Temporary local integer to hold the results of the query's total collection size
                //  This will be need by the Paginator during the paginator's execute
                int totalCount;

                RollingStockInfo = _rollingStockServices.GetByPartial(SearchArg,
                                            pageNumber, PAGE_SIZE, out totalCount);
                Pager = new Paginator(totalCount, current);
            }

        }

        public IActionResult OnPostFetch()
        {
            if (string.IsNullOrWhiteSpace(SearchArg))
            {
                Feedback = "Required:  Search argument is empty";
            }
            return RedirectToPage(new { SearchArg = SearchArg });
        }
    }
}
