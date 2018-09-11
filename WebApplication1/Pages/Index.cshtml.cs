using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        [BindProperty]
        public IList<Customer> Customers { get; private set; }

        [TempData]
        public string Message { get; set; }

        public IndexModel(AppDbContext db)
        {
            this._db = db;
        }

        public async Task OnGetAsync()
        {
            this.Customers = await this._db.Customer.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var customer = await _db.Customer.FindAsync(id);

            if(customer != null)
            {
                _db.Customer.Remove(customer);
                await _db.SaveChangesAsync();
            }
            
            return RedirectToPage();
        }
    }
}
