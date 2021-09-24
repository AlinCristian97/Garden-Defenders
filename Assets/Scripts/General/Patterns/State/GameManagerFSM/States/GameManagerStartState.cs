using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerStartState : GameManagerState
    {
        public override void Enter()
        {
            if (SpawnManager.Instance.gameObject.activeSelf)
            {
                SpawnManager.Instance.StartSpawningAttackers();
            }
            UIManager.Instance.HideShowCanvasGroup(UIManager.Instance.LevelProgressionSlider, true);
            GameManager.StateMachine.ChangeState(GameManager.States.OnGoingState);
        }

        public override void Exit()
        {
        }

        public override void Execute()
        {
        }
    }
}