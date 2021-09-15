namespace General.Patterns.State.DefenderFSM
{
    public class CombatDefenderAttackState : CombatDefenderState
    {
        public CombatDefenderAttackState(CombatDefender defender) : base(defender)
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
            if (CombatDefender.AttackCooldownPassed())
            {
                CombatDefender.UpdateNextAttack();
                CombatDefender.TriggerAttackAnimation();
            }

            if (!CombatDefender.HasTargetInAttackRange())
            {
                CombatDefender.StateMachine.ChangeState(CombatDefender.States.IdleState);
            }
        }
    }
}