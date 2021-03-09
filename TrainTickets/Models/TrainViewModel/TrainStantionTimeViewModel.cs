using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTickets.Models.TrainViewModel
{
    public class TrainStantionTimeViewModel
    {
        public IEnumerable<Train> trains {get;set;}
        public Train Trains { get; set; }
        public TimeSpan TimeSpan1 { get; set; }
        public TimeSpan TimeSpan2 { get; set; }
    }
}
