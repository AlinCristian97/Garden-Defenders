using System.Linq;
using General.Patterns.Singleton;
using UnityEngine;

namespace General.Patterns.State.GameManagerFSM.States
{
    public class GameManagerOnGoingState : GameManagerState
    {
        public override void Enter()
        {
            GameManager.StartSpawningPassiveEnergyResource();
        }

        public override void Exit()
        {
            GameManager.StopSpawningPassiveEnergyResource();
        }

        public override void Execute()
        {
            CheckWin();
            CheckLose();
        }

        private void CheckWin()
        {
            if (SpawnManager.Instance.HasFinishedSpawningWaves)
            {
                bool allEnemiesInLastWaveDead = SpawnManager.Instance.LastWaveAttackersList.All(x=>x.IsDead);

                if (allEnemiesInLastWaveDead)
                {
                    GameManager.StateMachine.ChangeState(GameManager.States.WinState);
                }
            }
        }

        private void CheckLose()
        {
            int attackerLayerMaskValue = (int) Mathf.Pow(2f, LayerMask.NameToLayer("Attacker"));

            if (GameManager.LoseCollider.IsTouchingLayers(attackerLayerMaskValue))
            {
                GameManager.StateMachine.ChangeState(GameManager.States.LoseState);
            }
        }
    }
}