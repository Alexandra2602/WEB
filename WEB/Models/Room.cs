using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string RoomType { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
