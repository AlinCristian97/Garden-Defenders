namespace FSM.States
{
    public class AttackerAttackState : AttackerState
    {
        public AttackerAttackState(Attacker attacker) : base(attacker)
        {
        }
    
        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Execute()
        {
            if (Attacker.AttackCooldownPassed())
            {
                Attacker.UpdateNextAttack();
                Attacker.TriggerAttackAnimation();
            }

            if (!Attacker.SetTargetInAttackRange())
            {
                Attacker.StateMachine.ChangeState(Attacker.States.WalkState);
            }
        }
    }
}