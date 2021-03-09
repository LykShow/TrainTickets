using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTickets.Models;
using TrainTickets.Data;

namespace TrainTickets
{
    public class EnsureCreated
    {
        public static void Initialize(ApplicationContext context)
        {
            if (!context.Trains.Any())
            {
                var num1 = new Place() { Name = "1" };
                var num2 = new Place() { Name = "2" };
                var num3 = new Place() { Name = "3" };
                var num4 = new Place() { Name = "4" };
                var num5 = new Place() { Name = "5" };
                context.Places.AddRange(num1, num2, num3, num4, num5);


                var vileyka = new Stantion() { Name = "Vileyka" };
                var polotsk = new Stantion() { Name = "Polotsk" };
                var vitebsk = new Stantion() { Name = "Vitebsk" };
                var molodechno = new Stantion() { Name = "Molodechno" };
                var minsk = new Stantion() { Name = "Minsk" };
                var krulevshizna = new Stantion() { Name = "Krulevshizna" };
                var podsvile = new Stantion() { Name = "Podsvile" };
                context.Stantions.AddRange(vileyka, polotsk, vitebsk, molodechno, minsk, krulevshizna, podsvile);


                var train1 = new Train { Name = "B867" };
                var train2 = new Train { Name = "A435" };
                var train3 = new Train { Name = "F877" };

                context.TrainStantions.AddRange(new TrainStantion { Train = train1, Stantion = minsk, Way = 1, Time = new TimeSpan(16,32,0)},
                    new TrainStantion { Train = train1, Stantion = vileyka, Way = 2, Time = new TimeSpan(16, 50, 0) },
                 new TrainStantion { Train = train1, Stantion = krulevshizna, Way = 3, Time = new TimeSpan(17, 25, 0) },
                 new TrainStantion { Train = train1, Stantion = podsvile, Way = 4, Time = new TimeSpan(17, 44, 0) },
                 new TrainStantion { Train = train1, Stantion = polotsk, Way = 5, Time = new TimeSpan(18, 15, 0) },

                  new TrainStantion { Train = train2, Stantion = minsk, Way = 1, Time = new TimeSpan(13, 45, 0) },
                 new TrainStantion { Train = train2, Stantion = molodechno, Way = 2, Time = new TimeSpan(14, 25, 0) },
                 new TrainStantion { Train = train2, Stantion = vileyka, Way = 3, Time = new TimeSpan(14, 35, 0) },
                 new TrainStantion { Train = train2, Stantion = krulevshizna, Way = 4, Time = new TimeSpan(14, 55, 0) },
                 new TrainStantion { Train = train2, Stantion = podsvile, Way = 5, Time = new TimeSpan(15, 15, 0)},
                 new TrainStantion { Train = train2, Stantion = polotsk, Way = 6, Time = new TimeSpan(15, 55, 0)},
                 new TrainStantion { Train = train2, Stantion = vitebsk, Way = 7, Time = new TimeSpan(16, 15, 0) },

                 new TrainStantion { Train = train3, Stantion = polotsk, Way = 1, Time = new TimeSpan(11, 55, 0) },
                  new TrainStantion { Train = train3, Stantion = podsvile, Way = 2, Time = new TimeSpan(12, 15, 0) },
                  new TrainStantion { Train = train3, Stantion = krulevshizna, Way = 3, Time = new TimeSpan(12, 44, 0) },                
                 new TrainStantion { Train = train3, Stantion = vileyka, Way = 4, Time = new TimeSpan(13, 10, 0) },
                 new TrainStantion { Train = train3, Stantion = minsk, Way = 5, Time = new TimeSpan(13, 25, 0) });
                
                context.SaveChanges();
                List<DateTime> dateTimes = new List<DateTime>() { };

                int t = 7;
                int f = 0;
                while (t != 0)
                {
                    dateTimes.Add(DateTime.Today.AddDays(f++));
                    t--;
                }
                foreach (var d in dateTimes)
                {
                    foreach (var s in context.Trains)
                    {
                        foreach (var i in context.Places)
                        {
                            context.TrainPlaces.Add(new TrainPlace { TrainId = s.Id, PlaceId = i.Id, Free = true, DateTime = d.Date });
                        }
                    }
                }


                context.SaveChanges();
            }
            else
            {

                ChangeTime(context.TrainPlaces);
                context.SaveChanges();
            }
            
        }
        private static void ChangeTime(IEnumerable<TrainPlace> trainPlaces)
        {
            foreach (var s in trainPlaces.Where(i => i.DateTime < DateTime.Today))
            {
                s.Free = true;
                s.DateTime = DateTime.Today.AddMonths(+1);

            }
            
        }
    }
}
