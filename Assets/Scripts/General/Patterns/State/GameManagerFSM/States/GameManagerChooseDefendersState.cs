using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerChooseDefendersState : GameManagerState
    {
        public override void Enter()
        {
            Debug.Log("Game: Enter ChooseDefenders");
        }

        public override void Exit()
        {
            Debug.Log("Game: Exit ChooseDefenders");
        }

        public override void Execute()
        {
            Debug.Log("Game: Execute ChooseDefenders");
        }
    }
}