using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerLoseState : GameManagerState
    {
        public override void Enter()
        {
            Debug.Log("Game: Enter Lose");
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