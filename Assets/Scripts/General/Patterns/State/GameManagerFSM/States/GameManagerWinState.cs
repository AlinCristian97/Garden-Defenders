using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerWinState : GameManagerState
    {
        public override void Enter()
        {
            Debug.Log("Game: Enter Win");
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