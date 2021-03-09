using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTickets.Models
{
    public class Stantion
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TrainStantion> trains { get; set; } = new List<TrainStantion>();
        
       
    }
}
