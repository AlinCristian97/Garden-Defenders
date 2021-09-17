using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerLoseState : GameManagerState
    {
        public override void Enter()
        {
            Debug.Log("Game: Enter Lose");
            
            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.MainCanvas, false);
            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.LoseCanvas, true);
            
            SpawnManager.Instance.StopSpawningAttackers();
            GameManager.Instance.StartCoroutine(GameManager.Instance.DeactivateAllActiveDefenders());
            GameManager.Instance.StartCoroutine(GameManager.Instance.DeactivateAllLawnMowers());
        }

        public override void Exit()
        {
            Debug.Log("Game: Exit Lose");
        }

        public override void Execute()
        {
            Debug.Log("Game: Execute Lose");
        }
    }
}