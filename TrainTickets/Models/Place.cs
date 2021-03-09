using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTickets.Models
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Free { get; set; } = true;
        public ICollection<TrainPlace> TrainPlaces { get; set; } = new List<TrainPlace>();
       
    }
}
