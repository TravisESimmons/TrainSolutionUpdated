using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainWatchSystem.Entities;
using TrainWatchSystem.BLL;
using TrainWatchSystem.DAL;
namespace TrainWebApp.Pages
{
    public class AboutModel : PageModel
    {

        private readonly ILogger<AboutModel> _logger;

        private readonly TrainWatchServices _trainWatchServices;


        public AboutModel(ILogger<AboutModel> logger, TrainWatchServices trainWatchServices)
        {
            _logger = logger;
            _trainWatchServices = trainWatchServices;
        }

        public DbVersion Version { get; set; }
        public void OnGet()
        {
            Version = _trainWatchServices.GetDbVersion();
        }
               
    }
    
}
