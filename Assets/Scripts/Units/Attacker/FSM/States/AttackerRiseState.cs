using UnityEngine;

namespace FSM.States
{
    public class AttackerRiseState : AttackerState
    {
        public AttackerRiseState(Attacker attacker) : base(attacker)
        {
        }
    
        public override void Enter()
        {
            Attacker.Collider.enabled = false;
        }

        public override void Exit()
        {
            Attacker.Collider.enabled = true;
        }

        public override void Execute()
        {
        }
    }
}