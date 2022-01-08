using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB.Data;
using WEB.Models;

namespace WEB.Pages.Reservations
{
    public class CreateModel : ReservationCategoriesPageModel
    {
        private readonly WEB.Data.WEBContext _context;

        public CreateModel(WEB.Data.WEBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["RoomID"] = new SelectList(_context.Set<Room>(), "ID", "RoomType");
            var reservation = new Reservation();
            reservation.ReservationCategories = new List<ReservationCategory>();
            PopulateAssignedCategoryData(_context, reservation);
            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newReservation = new Reservation();
            if(selectedCategories !=null)
            {
                newReservation.ReservationCategories = new List<ReservationCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new ReservationCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newReservation.ReservationCategories.Add(catToAdd);

                }
            }
            if(await TryUpdateModelAsync<Reservation>(
                newReservation,
                "Reservation",
                i => i.FirstName, i => i.LastName,
                 i => i.CheckinDate, i => i.CheckoutDate, i => i.RoomsNr, i =>i.RoomID))
            {
                _context.Reservation.Add(newReservation);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedCategoryData(_context, newReservation);
            return Page();                    
        }
    }
}
