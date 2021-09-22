using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerChooseDefendersState : GameManagerState
    {
        public override void Enter()
        {
            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.SelectLevelDefendersCanvas, true);
        }

        public override void Exit()
        {
            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.SelectLevelDefendersCanvas, false);
        }

        public override void Execute()
        {
            if (GameManager.LevelDefendersConfirmed)
            {
                GameManager.StateMachine.ChangeState(GameManager.States.GetReadyState);
            }
        }
    }
}