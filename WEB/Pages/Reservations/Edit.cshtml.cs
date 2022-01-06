using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB.Data;
using WEB.Models;

namespace WEB.Pages.Reservations
{
    public class EditModel : ReservationCategoriesPageModel
    {
        private readonly WEB.Data.WEBContext _context;

        public EditModel(WEB.Data.WEBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Reservation = await _context.Reservation.Include(b => b.Room).Include(b => b.ReservationCategories).ThenInclude(b => b.Category).AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);


            if (Reservation == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Reservation);
            ViewData["RoomID"] = new SelectList(_context.Set<Room>(), "ID", "RoomType");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id==null)
            {
                return NotFound();
            }
            var ReservationToUpdate = await _context.Reservation.Include(i => i.Room).Include(i => i.ReservationCategories).ThenInclude(i => i.Category).FirstOrDefaultAsync(s => s.ID == id);
            if (ReservationToUpdate == null)
                {
                return NotFound();
               }
            if (await TryUpdateModelAsync<Reservation>(
                ReservationToUpdate,
                "Reservation",
                i => i.FirstName, i => i.LastName,
            i => i.CheckinDate, i => i.CheckoutDate, i => i.RoomsNr, i => i.Room))
            {
                UpdateReservationCategories(_context, selectedCategories, ReservationToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateReservationCategories(_context, selectedCategories, ReservationToUpdate);
            PopulateAssignedCategoryData(_context, ReservationToUpdate);
            return Page();
                
        }
    }
}
           