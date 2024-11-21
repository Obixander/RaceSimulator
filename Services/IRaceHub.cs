namespace Services
{
    public interface IRaceHub
    {
        Task StartRace(string name, int amountOfLaps);
        void RaceUpdate(object? sender, RaceUpdate e);
        void CarCrossedLine(object? sender, CarCrossedLine e);
    }
}
