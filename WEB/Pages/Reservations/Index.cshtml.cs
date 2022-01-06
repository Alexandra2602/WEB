using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB.Data;
using WEB.Models;

namespace WEB.Pages.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly WEB.Data.WEBContext _context;

        public IndexModel(WEB.Data.WEBContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservation { get;set; }
        public ReservationData ReservationD { get; set; }
        public int ReservationID { get; set; }
        public int CatgeoryID { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID)
        {
            ReservationD = new ReservationData();
            ReservationD.Reservations = await _context.Reservation.Include(b => b.Room).Include(b => b.ReservationCategories).ThenInclude(b => b.Category).AsNoTracking().OrderBy(b => b.FirstName).ToListAsync();
            
            if (id!=null)
            {
                ReservationID = id.Value;
                Reservation reservation = ReservationD.Reservations.Where(i=>i.ID ==id.Value).Single();
                ReservationD.Categories = reservation.ReservationCategories.Select(s => s.Category);

            }
        }
    }
}
