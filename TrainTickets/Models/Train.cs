using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTickets.Models
{
    public class Train
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TrainPlace> TrainPlaces { get; set; } = new List<TrainPlace>();
        public  ICollection<TrainStantion> stantions { get; set; } = new List<TrainStantion>();
        
        
    }
}
