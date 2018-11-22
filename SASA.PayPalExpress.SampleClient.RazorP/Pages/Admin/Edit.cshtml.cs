using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaymentGateways.PayPal;
using System.Linq;
using System.Threading.Tasks;

namespace SASA.PayPalExpress.SampleClient.RazorP.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly SASA.PayPalExpress.SampleClient.RazorP.Models.SASAPayPalExpressSampleClientRazorPContext _context;

        public EditModel(SASA.PayPalExpress.SampleClient.RazorP.Models.SASAPayPalExpressSampleClientRazorPContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PayPalBearersToken).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayPalBearersTokenExists(PayPalBearersToken.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PayPalBearersTokenExists(int id)
        {
            return _context.PayPalBearersToken.Any(e => e.Id == id);
        }
    }
}
