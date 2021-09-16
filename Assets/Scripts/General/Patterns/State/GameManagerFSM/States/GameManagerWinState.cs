using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerWinState : GameManagerState
    {
        public override void Enter()
        {
            Debug.Log("Game: Enter Win");
            
            //mark level as complete + unlock next level logic
            
            UIManager.Instance.HideShowCanvasGroup(UIManager.Instance.MainCanvas, false);
            UIManager.Instance.HideShowCanvasGroup(UIManager.Instance.WinCanvas, true);
        }

        public override void Exit()
        {
            Debug.Log("Game: Exit Win");
        }

        public override void Execute()
        {
            Debug.Log("Game: Execute Win");
        }
    }
}