namespace General.Patterns.State.DefenderFSM
{
    public class CombatDefenderIdleState : CombatDefenderState
    {
        public CombatDefenderIdleState(CombatDefender combatDefender) : base(combatDefender)
        {
        }

        public override void Enter()
        {
            CombatDefender.Animator.SetBool("IsIdling", true);
        }

        public override void Exit()
        {
            CombatDefender.Animator.SetBool("IsIdling", false);
        }

        public override void Execute()
        {
            if (CombatDefender.HasTargetInAttackRange() && CombatDefender.AttackCooldownPassed())
            {
                CombatDefender.StateMachine.ChangeState(CombatDefender.States.AttackState);
            }
        }
    }
}