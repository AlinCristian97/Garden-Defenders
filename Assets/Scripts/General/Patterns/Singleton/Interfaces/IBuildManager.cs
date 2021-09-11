using UnityEngine;

namespace General.Patterns.Singleton.Interfaces
{
    public interface IBuildManager
    {
        void BuildDefender(Vector3 buildPosition, Transform parent);
        void SellDefender();

    }
}