using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerStartState : GameManagerState
    {
        public override void Enter()
        {
            Debug.Log("Game: Enter Start");

            if (SpawnManager.Instance.gameObject.activeSelf)
            {
                SpawnManager.Instance.StartSpawningAttackers();
            }
            GameManager.StateMachine.ChangeState(GameManager.States.OnGoingState);
        }

        public override void Exit()
        {
            Debug.Log("Game: Exit Start");
        }

        public override void Execute()
        {
            Debug.Log("Game: Execute Start");
        }
    }
}