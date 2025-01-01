using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JudgeRegistrationRazor.Data;
using JudgeRegistrationRazor.Models;

namespace JudgeRegistrationRazor.Pages.JudgesRegistration
{
    public class DetailsModel : PageModel
    {
        private readonly OdysseyContext _context;

        public DetailsModel(OdysseyContext context)
        {
            _context = context;
        }

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
            else
            {
                Judge = judge;
            }
            return Page();
        }
    }
}
