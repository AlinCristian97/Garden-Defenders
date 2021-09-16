using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerGetReadyState : GameManagerState
    {
        public override void Enter()
        {
            Debug.Log("Game: Enter GetReady");
            
            UIManager.Instance.HideShowCanvasGroup(UIManager.Instance.MainCanvas, true);
        }

        public override void Exit()
        {
            Debug.Log("Game: Exit GetReady");
        }

        public override void Execute()
        {
            Debug.Log("Game: Execute GetReady");
        }
    }
}