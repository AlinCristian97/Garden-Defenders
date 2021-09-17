using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerGetReadyState : GameManagerState
    {
        private float _gameStartTime;       

        public override void Enter()
        {
            Debug.Log("Game: Enter GetReady");

            _gameStartTime = Time.time + GameManager.Instance.GetReadyTimeInSeconds;
            
            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.MainCanvas, true);
        }
        
        public override void Exit()
        {
            Debug.Log("Game: Exit GetReady");
        }

        public override void Execute()
        {
            Debug.Log("Game: Execute GetReady");

            if (Time.time >= _gameStartTime)
            {
                GameManager.StateMachine.ChangeState(GameManager.States.StartState);
            }
        }
    }
}