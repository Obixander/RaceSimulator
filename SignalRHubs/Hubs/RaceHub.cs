using Entities;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Services;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

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
                string json = JsonConvert.SerializeObject(e.race);
                await Clients.All.SendAsync("RaceUpdate", json);

            }
        }
        public async void CarCrossedLine(object? sender, CarCrossedLine e)
        {
            if (Clients.All != null && Clients != null)
            {
                Console.WriteLine("Car crossed line event triggered.");
                string json = JsonConvert.SerializeObject(e.racers);
                await Clients.All.SendAsync("CarCrossedLine", json); //cant send this datatype fix
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
