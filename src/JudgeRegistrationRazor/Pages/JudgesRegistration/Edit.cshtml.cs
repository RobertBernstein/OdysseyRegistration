using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JudgeRegistrationRazor.Data;
using JudgeRegistrationRazor.Models;

namespace JudgeRegistrationRazor.Pages.JudgesRegistration
{
    public class EditModel : PageModel
    {
        private readonly OdysseyContext _context;

        public EditModel(OdysseyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Judge Judge { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var judge = await _context.Judges.FirstOrDefaultAsync(m => m.Id == id);
            if (judge == null)
            {
                return NotFound();
            }
            Judge = judge;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Judge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JudgeExists(Judge.Id))
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

        private bool JudgeExists(int id)
        {
            return _context.Judges.Any(e => e.Id == id);
        }
    }
}
