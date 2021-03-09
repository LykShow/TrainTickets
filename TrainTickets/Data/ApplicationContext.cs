using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTickets.Models;

namespace TrainTickets.Data
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Train> Trains { get; set; }
       public DbSet<Stantion> Stantions { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<TrainPlace> TrainPlaces { get; set; }
       public DbSet<TrainStantion> TrainStantions { get; set; }
       
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();

        }
    }
}
