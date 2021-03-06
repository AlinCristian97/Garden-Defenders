using System.Collections;
using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerGetReadyState : GameManagerState
    {
        private float _gameStartTime;       

        public override void Enter()
        {
            _gameStartTime = Time.time + GameManager.Instance.GetReadyTimeInSeconds;
            
            UIManager.Instance.ActivateDeactivateCanvas(UIManager.Instance.MainCanvas, true);
            
            if (WarnMessageManager.Instance != null)
            {
                WarnMessageManager.Instance.SpawnWarningMessage("Zombies are approaching", 3f);
            }
        }
        
        public override void Exit()
        {
        }

        public override void Execute()
        {
            if (Time.time >= _gameStartTime)
            {
                GameManager.StateMachine.ChangeState(GameManager.States.StartState);
            }
        }
    }
}