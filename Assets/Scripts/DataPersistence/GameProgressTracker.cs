using System;
using General.Patterns.Singleton;

namespace General
{
    [Serializable]
    public class GameProgressTracker
    {
        public int HighestLevelUnlocked;

        public GameProgressTracker()
        {
            HighestLevelUnlocked = 1;
        }
        
        public void UpdateHighestLevelUnlocked()
        {
            if (GameManager.Instance.CurrentLevel >= HighestLevelUnlocked)
            {
                HighestLevelUnlocked++;
            }
        }
    }
}