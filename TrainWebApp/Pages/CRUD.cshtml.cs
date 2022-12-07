#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Drawing.Printing;
using TrainWatchSystem.BLL;
using TrainWatchSystem.Entities;

namespace TrainWebApp.Pages
{
    public class CRUDModel : PageModel
    {
        #region Private service fields & class constructor
            private readonly ILogger<IndexModel> _logger;
            private readonly RollingStockServices _rollingStockServices;
            private readonly TrainWatchServices _trainWatchServices;

            public CRUDModel(ILogger<IndexModel> logger, RollingStockServices rollingStockServices, TrainWatchServices trainWatchServices)
            {
                _logger = logger;
                _rollingStockServices = rollingStockServices;
                _trainWatchServices = trainWatchServices;
            }
            #endregion

            [TempData]
            public string Feedback { get; set; }

            public bool HasFeedback => !string.IsNullOrWhiteSpace(Feedback);

            public string ErrorMessage { get; set; }

            public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

            [BindProperty(SupportsGet = true)]

            public string ReportingMark { get; set; }

            [BindProperty(SupportsGet = true)]

            public string SearchArg { get; set; }

            [BindProperty(SupportsGet = true)]
            public int? searcharg{ get; set; }

            [BindProperty]

            public RollingStock RollingStockInfo { get; set; }

            [BindProperty]
            public List<RailCarType> RailCarTypes { get; set; }

            [BindProperty(SupportsGet = true)]
            public int RailCarTypeID { get; set; }

        private void PopulateLists()
        {
            RailCarTypes = _rollingStockServices.RailCarTypeList();
        }

        public void OnGet()
        {
            //  The OnGet executes the first time the page is generated
            //      then on each Get request issued  by the page (such as on RedirectToPage())
            PopulateLists();

            if (!string.IsNullOrWhiteSpace(ReportingMark))
            {
                RollingStockInfo = _rollingStockServices.GetByID(ReportingMark);
            }

        }

        public IActionResult OnPostDelete()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string ReportingMark = _rollingStockServices.RollingStock_DeleteStock(RollingStockInfo);
                    if (ReportingMark != null)
                    {
                        Feedback = $"Reporting Mark ({ReportingMark}) is no longer in use.";
                    }
                    else
                    {
                        Feedback = "No Rolling Stock was affected.  Refresh search and try again.";
                    }
                    return RedirectToPage(new { ReportingMark = ReportingMark });
                }
                catch (ArgumentNullException ex)
                {
                    ErrorMessage = GetInnerException(ex).Message;
                    PopulateLists();
                    return Page();
                }
                catch (Exception ex)
                {
                    ErrorMessage = GetInnerException(ex).Message;
                    PopulateLists();
                    return Page();
                }
            }

            return Page();
        }

        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;

            }
            return ex;
        }



    }
    
}
