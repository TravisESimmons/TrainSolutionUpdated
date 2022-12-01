using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrainWebApp.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public string UserEmail{ get; set; }

        [BindProperty]
        public string SubjectOrTopic { get; set; }

        [BindProperty]
        public string MessageTitle { get; set; }

        [BindProperty]

        public string MessageBody { get; set; }

        // Attribute TempData is required IF you are processing mulitple requests. 
        // Followed by OnGet() to retain data within the property.
        // [TempData] 
        
        public void OnGet()
        {
        }
    }
}
