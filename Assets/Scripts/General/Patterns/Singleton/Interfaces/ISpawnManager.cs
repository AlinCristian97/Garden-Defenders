namespace General.Patterns.Singleton.Interfaces
{
    public interface ISpawnManager
    {
        public int NumberOfWaves { get; }
        public float StartDelay { get; }
        public float TimeBetweenWaves { get; }
    }
}