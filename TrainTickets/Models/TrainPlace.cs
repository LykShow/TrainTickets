using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTickets.Models
{
    public class TrainPlace
    {
        public int Id { get; set; }
        public int TrainId { get; set; }
        public int PlaceId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public Train Train { get; set; }
        public Place Place { get; set; }

        public DateTime DateTime { get; set; }
        public bool Free { get; set; } = true;
    }
}
