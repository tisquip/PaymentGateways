using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaymentGateways.PayPal;
using System.Threading.Tasks;

namespace SASA.PayPalExpress.SampleClient.RazorP.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly SASA.PayPalExpress.SampleClient.RazorP.Models.SASAPayPalExpressSampleClientRazorPContext _context;

        public DeleteModel(SASA.PayPalExpress.SampleClient.RazorP.Models.SASAPayPalExpressSampleClientRazorPContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PayPalBearersToken PayPalBearersToken { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PayPalBearersToken = await _context.PayPalBearersToken.SingleOrDefaultAsync(m => m.Id == id);

            if (PayPalBearersToken == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PayPalBearersToken = await _context.PayPalBearersToken.FindAsync(id);

            if (PayPalBearersToken != null)
            {
                _context.PayPalBearersToken.Remove(PayPalBearersToken);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
