using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTickets.Models;
using TrainTickets.Data;
using TrainTickets.Models.TrainViewModel;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace TrainTickets.Controllers
{
    public class TrainController : Controller
    {
       
        private IContain contain;
        public TrainController(IContain contain)
        {
            this.contain = contain;
           
          
        }

        public IActionResult Train(string useid, string searchFrom, string searchWhere, DateTime dateTime)
        {
            ViewData["CurrentSta"] = searchFrom ?? "";
            ViewData["NextSta"] = searchWhere ?? "";
            if(dateTime == default)
            {
                dateTime = DateTime.Today;
            }
            ViewData["Date"] = dateTime;
            List<TrainStantionTimeViewModel> trainStantionTimeViews1 = new List<TrainStantionTimeViewModel>();
            foreach(var s in contain.Trains)
            {
                trainStantionTimeViews1.Add(new TrainStantionTimeViewModel { Trains = contain.Trains.Single(i => i.Id == s.Id) });
            }
            IEnumerable<TrainStantionTimeViewModel> trainStantionTimes = trainStantionTimeViews1;

            if (!string.IsNullOrEmpty(searchFrom))
            {
                var stantionnum1 = contain.Stantions.Where(i => i.Name == searchFrom).FirstOrDefault();
                var stationnum2 = contain.Stantions.Where(i => i.Name == searchWhere).FirstOrDefault();


                List<Train> trains = new List<Train>();
               
                
                foreach (var s in Search(stantionnum1, stationnum2, dateTime))
                {
                    
                    
                    trains.Add(contain.Trains.Single(i => i.Id == s.TrainId));
                }
                List<TrainStantionTimeViewModel> trainStantionTimeViews2 = new List<TrainStantionTimeViewModel>();
                foreach(var s in trains.Distinct())
                {
                    trainStantionTimeViews2.Add(new TrainStantionTimeViewModel { Trains = contain.Trains.Single(i => i.Id == s.Id), TimeSpan1 = contain.TrainStantions.Single(i=>i.TrainId== s.Id&&i.StantionId==stantionnum1.Id).Time, TimeSpan2 = contain.TrainStantions.Single(i => i.TrainId == s.Id && i.StantionId == stationnum2.Id).Time });
                }
                trainStantionTimes = trainStantionTimeViews2;
            }
            ViewBag.UserId = useid;
            return View(trainStantionTimes);
        }
        private IEnumerable<TrainPlace> Search(Stantion stantion1, Stantion stantion2, DateTime dateTime)
        {
            
            
            List<int> tr1 = new List<int>();
            tr1.AddRange(contain.TrainStantions.Where(i => i.StantionId == stantion1.Id).Select(i => i.Way));
            List<int> tr2 = new List<int>();
            tr2.AddRange(contain.TrainStantions.Where(i => i.StantionId == stantion2.Id).Select(i => i.Way));
            int i = 0;
            List<TrainStantion> trains = new List<TrainStantion>();
            foreach (var s in tr1)
            {
                if (s <= tr2[i])
                {
                    
                  var trainstantion = contain.TrainStantions.Where(i => i.StantionId == stantion1.Id && i.Way == s);
                    foreach(var t in trainstantion)
                    {
                        var st = contain.TrainStantions.Single(i => i.TrainId == t.TrainId && i.StantionId == stantion2.Id);
                        if (t.Way <=st.Way)
                        {
                            trains.Add(t);
                        }
                    }
                }
                i++;
            }
            List<TrainPlace> trains1 = new List<TrainPlace>();
            foreach(var s in trains)
            {
                trains1.AddRange(contain.TrainPlaces.Where(i => i.TrainId == s.TrainId && i.DateTime == dateTime));
            }
            
            
            return trains1;

        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string useid, DateTime dateTime)
        {
            
            if (id == null)
            {
                return RedirectToAction(nameof(Train));
            }
            DateTime dateTime1 = ChangeDate(dateTime);
            TrainPlaceViewModel trainPlaceViewModel = new TrainPlaceViewModel();
             trainPlaceViewModel.Current= await contain.Trains.SingleAsync(s => s.Id == id);
            trainPlaceViewModel.trainPlaces = contain.TrainPlaces.Where(i => i.TrainId == id && i.DateTime == dateTime1).OrderBy(i => i.PlaceId);

            trainPlaceViewModel.places= trainPlaceViewModel.trainPlaces.Select(i=>i.Place);
            ViewBag.Date = dateTime1;
            ViewBag.UserId = useid;
            return View(trainPlaceViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Details(int id, int[] place , DateTime dateTime, string useid)
        {
            DateTime dateTime1 = ChangeDate(dateTime);
            var train = await contain.Trains.SingleAsync(s => s.Id == id);

            foreach(var i in place)
            {
                var trainplace = await contain.TrainPlaces.SingleAsync(s => s.TrainId == id && s.PlaceId == i && s.DateTime == dateTime1);
                trainplace.Free = false;
                trainplace.UserId = useid;
                await contain.UpdateTrainPlace(trainplace);
            }
            

            
            return RedirectToAction(nameof(Train));
        }
        private static DateTime ChangeDate(DateTime dateTime)
        {
            DateTime dateTime1;
            var s = dateTime.ToString("dd.MM.yyyy");
            DateTime.TryParse(s, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime1);
            return dateTime1;
        }
    }
}
