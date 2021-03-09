using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTickets.Models;

namespace TrainTickets.Data
{
    public class EFRepository:IContain
    {
        public IQueryable<Train> Trains => context.Trains;
        public IQueryable<Stantion> Stantions => context.Stantions;

        public IQueryable<Place> Places => context.Places;

        public IQueryable<TrainPlace> TrainPlaces => context.TrainPlaces;

        public IQueryable<TrainStantion> TrainStantions => context.TrainStantions;

        public IQueryable<User> Users => context.Users;

        private ApplicationContext context;
        public EFRepository(ApplicationContext applicationContext)
        {
            context = applicationContext;
        }
        public void Save()
        {
            
            context.SaveChanges();
        }

        public Task AddTrainPlace(TrainPlace trainPlace)
        {
            context.Add(trainPlace);
            return context.SaveChangesAsync();
        }

        public Task DeleteTrainPlace(TrainPlace trainPlace)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTrainPlace(TrainPlace trainPlace)
        {
            context.Update(trainPlace);
            return context.SaveChangesAsync();
        }
    }
}
