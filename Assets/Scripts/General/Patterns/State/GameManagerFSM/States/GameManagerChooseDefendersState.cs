using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerChooseDefendersState : GameManagerState
    {
        public override void Enter()
        {
            Debug.Log("Game: Enter ChooseDefenders");
            
            UIManager.Instance.HideShowCanvasGroup(UIManager.Instance.SelectLevelDefendersCanvas, true);
        }

        public override void Exit()
        {
            Debug.Log("Game: Exit ChooseDefenders");
            
            UIManager.Instance.HideShowCanvasGroup(UIManager.Instance.SelectLevelDefendersCanvas, false);
        }

        public override void Execute()
        {
            Debug.Log("Game: Execute ChooseDefenders");

            if (GameManager.LevelDefendersConfirmed)
            {
                Debug.Log("LevelDefenders have been confirmed");
                GameManager.StateMachine.ChangeState(GameManager.States.GetReadyState);
            }
        }
    }
}