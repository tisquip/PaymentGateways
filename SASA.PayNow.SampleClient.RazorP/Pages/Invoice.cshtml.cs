using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SASA.PayNow.SampleClient.RazorP.Models;

namespace SASA.PayNow.SampleClient.RazorP.Pages
{
    public class InvoiceModel : PageModel
    {
        private string baseUrl = "https://localhost:44365";

        [FromRoute]
        public string Reference { get; set; }
        private readonly ApplicationDbContext applicationDbContext;
        public PayNowObject PayNowObject { get; set; }

        [BindProperty]
        public string SelectedReference { get; set; }

        public InvoiceModel(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task OnGetAsync()
        {
            PayNowObject = await applicationDbContext.PayNowObject.SingleOrDefaultAsync(p => !String.IsNullOrWhiteSpace(p.Reference) && p.Reference == Reference);
        }

        public async Task<IActionResult> OnPostPollAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync($"{baseUrl}/api/paynow/poll/{Reference}");
            }
            return RedirectToPage();
        }
    }
}