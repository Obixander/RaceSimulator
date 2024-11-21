using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Entities;
namespace Services
{

    public class RaceUpdate : EventArgs
    {
        public Race race;
    }

    public class CarCrossedLine : EventArgs
    {
        public List<Car> racers;
    }

    public class RaceMananger
    {
        public event EventHandler<RaceUpdate> RaceUpdate;
        public event EventHandler<CarCrossedLine> CarCrossedLine;

        public async Task StartRace(string NameOfRace, int AmountOfLaps)
        {

            Race race = new Race(NameOfRace, GetRacers(), AmountOfLaps);
            Console.WriteLine(race.Name + " " + race.AmountOfLaps);
            OnRaceUpdate(new RaceUpdate
            {
                race = race
            });

            Stopwatch Laptime = new();

            while (race.CurrentLap <= race.AmountOfLaps)
            {
                Console.WriteLine($"Lap: {race.CurrentLap} / {race.AmountOfLaps}");
                Laptime.Start();

                //first wait between 50 to 60 seconds for the lapTime
                await Task.Delay(RandomCalc()).ConfigureAwait(false);

                Random rng = new Random();
                race.Racers = race.Racers.OrderBy(_ => rng.Next()).ToList();

                int placement = 1;
                foreach (var racer in race.Racers)
                {
                    if (racer.Laps[race.CurrentLap - 1] == default)
                    {
                        racer.LastLap = (long)Laptime.Elapsed.TotalMilliseconds;
                        racer.Laps[race.CurrentLap - 1] = (long)Laptime.Elapsed.TotalMilliseconds;
                        racer.Placement = placement;


                        foreach (var laptime in racer.Laps)
                        {
                            if (laptime < racer.BestLap || racer.BestLap == default)
                            {
                                if (laptime != default)
                                    racer.BestLap = laptime;
                            }
                        }
                    }

                    //sends the updated standings
                    OnCarCrossedLine(new CarCrossedLine
                    {
                        racers = race.Racers
                    });
                    placement++;
                    Console.WriteLine($"{racer.Name} - Placement: {racer.Placement} - Lap Time: {racer.LastLap} - Best Time: {racer.BestLap}");
                    await Task.Delay(RandomWait()).ConfigureAwait(false);
                }
                Laptime.Reset();
                Console.WriteLine("");
                race.CurrentLap++;
                OnRaceUpdate(new RaceUpdate
                {
                    race = race
                });
                await Task.Yield();

            }
            race.Finished = true;
            OnRaceUpdate(new RaceUpdate
            {
                race = race
            });
        }
        private static int RandomCar(int Max)
        {
            Random rand = new();
            return rand.Next(0, Max);
        }
        private static int RandomCalc()
        {
            Random rand = new();
            return rand.Next(25000, 26001); //returns between 50000 ms and 60000 ms (50 to 60 seconds)
        }

        private static int RandomWait()
        {
            Random rand = new();
            return rand.Next(100, 9001);
        }

        private List<Car> GetRacers()
        {
            List<Car> cars = new List<Car>();
            cars.Add(new Car("Ferrari SF-23", 5));
            cars.Add(new Car("McLaren MCL-35", 5));
            cars.Add(new Car("Mercedes W12", 5));
            cars.Add(new Car("Red Bull RB16B", 5));
            cars.Add(new Car("Porsche 911 GT3 R", 5));
            cars.Add(new Car("Ferrari 296 GT3", 5));
            cars.Add(new Car("Lamborghini Huracán GT3 EVO2", 5));
            cars.Add(new Car("BMW M4 GT3", 5));

            return cars;

        }
        protected virtual void OnRaceUpdate(RaceUpdate e)
        {
            RaceUpdate?.Invoke(this, e);
        }
        protected virtual void OnCarCrossedLine(CarCrossedLine e)
        {
            Console.WriteLine("Car crossed line - firing event...");
            CarCrossedLine?.Invoke(this, e);
        }
    }
}
