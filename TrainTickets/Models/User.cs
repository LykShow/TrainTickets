using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTickets.Models
{
    public class User: IdentityUser
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string SoName { get; set; }
        public TrainPlace TrainPlace { get; set; }
    }
}
