namespace General.Patterns.Singleton.Interfaces
{
    public interface ISpawnManager
    {
        public int NumberOfWaves { get; }
        public float TimeBetweenWaves { get; }
    }
}