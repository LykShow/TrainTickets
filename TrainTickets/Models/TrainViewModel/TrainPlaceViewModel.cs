using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTickets.Models.TrainViewModel
{
    public class TrainPlaceViewModel
    {
        public IEnumerable<Place> places { get; set; }
        public IEnumerable<TrainPlace> trainPlaces {get;set;}
        public Train Current { get; set; }
        
    }
}
