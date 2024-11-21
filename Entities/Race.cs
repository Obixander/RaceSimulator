using System.Diagnostics;

namespace Entities
{
    public class Race
    {
        private string name; //the name of the race
        private List<Car> racers; //the list of cars in the race
        private int amountOfLaps; //the amount of laps in the race
        private int currentLap = 1;
        private bool finished = false;
        public Race(string name, List<Car> racers, int amountOfLaps)
        {
            Name = name;
            Racers = racers;
            AmountOfLaps = amountOfLaps;
        }
        public Race()
        {

        }
        public string Name { get => name; set => name = value; }
        public List<Car> Racers { get => racers; set => racers = value; }
        public int AmountOfLaps { get => amountOfLaps; set => amountOfLaps = value; }
        public int CurrentLap { get => currentLap; set => currentLap = value; }
        public bool Finished { get => finished; set => finished = value; }
    }
}
