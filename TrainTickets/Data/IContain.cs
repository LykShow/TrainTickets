using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTickets.Models;

namespace TrainTickets.Data
{
    public interface IContain
    {
        IQueryable<Train> Trains { get; }
        IQueryable<Stantion> Stantions { get; }
        IQueryable<Place> Places { get; }
        IQueryable<TrainPlace> TrainPlaces { get; }
       IQueryable<TrainStantion> TrainStantions { get; }
        IQueryable<User> Users { get; }
        Task AddTrainPlace(TrainPlace trainPlace);
        Task DeleteTrainPlace(TrainPlace trainPlace);
        void Save();
        Task UpdateTrainPlace(TrainPlace trainPlace);
    }
}
