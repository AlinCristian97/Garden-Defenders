using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerOnGoingState : GameManagerState
    {
        public override void Enter()
        {
            Debug.Log("Game: Enter OnGoing");
        }

        public override void Exit()
        {
            Debug.Log("Game: Exit OnGoing");
        }

        public override void Execute()
        {
            Debug.Log("Game: Execute OnGoing");
        }
    }
}