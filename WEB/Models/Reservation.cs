using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        [RegularExpression(@"^[A-Z][a-z]+$", ErrorMessage = "Prenumele trebuie sa inceapa cu litera mare"), Required, StringLength(150, MinimumLength = 3)]
        public string FirstName { get; set; }
        [RegularExpression(@"^[A-Z][a-z]+$", ErrorMessage ="Numele trebuie sa inceapa cu litera mare"),Required,StringLength(150,MinimumLength =3)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        [Display(Name = "NumberOfRooms")]
        public int RoomsNr { get; set; }

        public int RoomID { get; set; }
        public Room Room { get; set; }
        
        public ICollection<ReservationCategory> ReservationCategories { get; set; }
    }
}
