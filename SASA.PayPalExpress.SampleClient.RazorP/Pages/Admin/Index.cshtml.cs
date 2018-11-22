using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaymentGateways.PayPal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SASA.PayPalExpress.SampleClient.RazorP.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly SASA.PayPalExpress.SampleClient.RazorP.Models.SASAPayPalExpressSampleClientRazorPContext _context;

        public IndexModel(SASA.PayPalExpress.SampleClient.RazorP.Models.SASAPayPalExpressSampleClientRazorPContext context)
        {
            _context = context;
        }

        public IList<PayPalBearersToken> PayPalBearersToken { get; set; }

        public async Task OnGetAsync()
        {
            PayPalBearersToken = await _context.PayPalBearersToken.ToListAsync();
        }
    }
}
