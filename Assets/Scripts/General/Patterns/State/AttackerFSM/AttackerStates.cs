using General.Patterns.State.AttackerFSM.States;

namespace General.Patterns.State.AttackerFSM
{
    public class AttackerStates
    {
        public AttackerIdleState IdleState { get; }
        public AttackerWalkState WalkState { get; }
        public AttackerAttackState AttackState { get; }
        public AttackerDeadState DeadState { get; }
        public AttackerRiseState RiseState { get; }

    
        public AttackerStates(Attacker attacker)
        {
            IdleState = new AttackerIdleState(attacker);
            WalkState = new AttackerWalkState(attacker);
            AttackState = new AttackerAttackState(attacker);
            DeadState = new AttackerDeadState(attacker);
            RiseState = new AttackerRiseState(attacker);
        }
    }
}