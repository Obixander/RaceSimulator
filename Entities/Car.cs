using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Car
    {
        private string name; //the name of the car
        private TimeSpan lastLap; //the time of the last lap took to complete
        private TimeSpan bestLap; //the best lap time of the car so far in the race
        private TimeSpan[] laps; //the array of all the lap times of the car in the race
        private int? placement = 0; //the placement of the car in the race so far
        public Car(string name, int amountOfLaps)
        {
            Name = name;
            Laps = new TimeSpan[amountOfLaps];
        }

        public string Name { get => name; set => name = value; }
        public TimeSpan LastLap { get => lastLap; set => lastLap = value; }
        public TimeSpan BestLap { get => bestLap; set => bestLap = value; }
        public TimeSpan[] Laps { get => laps; set => laps = value; }
        public int? Placement { get => placement; set => placement = value; }

        public TimeSpan TotalTime()
        {
            TimeSpan totalTime = new();

            foreach (TimeSpan lap in Laps)
            {
                totalTime += lap;
            }
            return totalTime;
        }
    }
}
