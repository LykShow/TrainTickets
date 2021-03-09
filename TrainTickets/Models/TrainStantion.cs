using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTickets.Models
{
    public class TrainStantion
    {
        public int Id { get; set; }
        public int Way { get; set; }
        public TimeSpan Time { get; set; }
        public int TrainId { get; set; }
        public int StantionId { get; set; }
        public Train Train { get; set; }
        public Stantion Stantion { get; set; }
    }
}
