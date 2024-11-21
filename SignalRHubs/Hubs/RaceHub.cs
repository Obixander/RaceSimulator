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
           await race.StartRace(name, amountOfLaps);
        }

        public async void RaceUpdate(object? sender, RaceUpdate e)
        {
            if (Clients.All != null && Clients != null)
            {
                Console.WriteLine($"Sending race update: {e.race.Name}");
                await Clients.All.SendAsync(nameof(RaceUpdate), e.race);

            }
        }
        public async void CarCrossedLine(object? sender, CarCrossedLine e)
        {
            if (Clients.All != null && Clients != null)
            {
                Console.WriteLine($"Sending car crossed line update.");
                await Clients.All.SendAsync(nameof(CarCrossedLine), e.racers);
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
