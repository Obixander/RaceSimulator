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
        private long lastLap; //the time of the last lap took to complete
        private long bestLap; //the best lap time of the car so far in the race
        private long[] laps; //the array of all the lap times of the car in the race
        private int? placement = 0; //the placement of the car in the race so far
        public Car(string name, int amountOfLaps)
        {
            Name = name;
            Laps = new long[amountOfLaps];
        }

        public string Name { get => name; set => name = value; }
        public long LastLap { get => lastLap; set => lastLap = value; }
        public long BestLap { get => bestLap; set => bestLap = value; }
        public long[] Laps { get => laps; set => laps = value; }
        public int? Placement { get => placement; set => placement = value; }

        public long TotalTime()
        {
            long totalTime = new();

            foreach (long lap in Laps)
            {
                totalTime += lap;
            }
            return totalTime;
        }
    }
}
