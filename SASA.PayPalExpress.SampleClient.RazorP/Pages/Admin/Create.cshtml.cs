using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentGateways.PayPal;
using System.Threading.Tasks;

namespace SASA.PayPalExpress.SampleClient.RazorP.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly SASA.PayPalExpress.SampleClient.RazorP.Models.SASAPayPalExpressSampleClientRazorPContext _context;

        public CreateModel(SASA.PayPalExpress.SampleClient.RazorP.Models.SASAPayPalExpressSampleClientRazorPContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PayPalBearersToken PayPalBearersToken { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PayPalBearersToken.Add(PayPalBearersToken);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}