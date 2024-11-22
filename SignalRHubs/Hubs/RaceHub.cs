using Entities;
using Microsoft.AspNetCore.SignalR;
using Services;
using System.Runtime.InteropServices;

namespace SignalRHubs.Hubs
{
    public class RaceHub : Hub, IRaceHub
    {
        private readonly RaceMananger race;
        public RaceHub(RaceMananger race)
        {
            this.race = race;

            this.race.RaceUpdate += RaceUpdate;
            this.race.CarCrossedLine += CarCrossedLine;
            
        }
        public async Task StartRace(string name, int amountOfLaps)
        {
            if (Clients.All != null && Clients != null)
            {
               await race.StartRace(name, amountOfLaps);
            }

        }

        public async void RaceUpdate(object? sender, RaceUpdate e)
        {
            if (Clients.All != null && Clients != null)
            {
                Console.WriteLine($"Race update triggered. Sending race: {e.race.Name}, Current lap: {e.race.CurrentLap}");

                // Convert TimeSpan to long if it's not done yet
                foreach (var car in e.race.Racers)
                {
                    Console.WriteLine($"Car: {car.Name}, BestLapMilliseconds: {car.BestLap}");
                }
                await Clients.All.SendAsync("RaceUpdate", new {e.race});

            }
        }
        public async void CarCrossedLine(object? sender, CarCrossedLine e)
        {
            if (Clients.All != null && Clients != null)
            {
                Console.WriteLine("Car crossed line event triggered.");

                foreach (var car in e.racers)
                {
                    Console.WriteLine($"Car: {car.Name}, BestLapMilliseconds: {car.BestLap}");
                }
                await Clients.All.SendAsync("CarCrossedLine", new object[] { e.racers }); //cant send this datatype fix
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.race.RaceUpdate -= RaceUpdate;
                this.race.CarCrossedLine -= CarCrossedLine;
            }
            base.Dispose(disposing);
        }

    }
}
