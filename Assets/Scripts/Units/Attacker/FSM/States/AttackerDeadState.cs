namespace FSM.States
{
    public class AttackerDeadState : AttackerState
    {
        public AttackerDeadState(Attacker attacker) : base(attacker)
        {
        }
    
        public override void Enter()
        {
            Attacker.Animator.SetTrigger("Die");
        }

        public override void Exit()
        {
        }

        public override void Execute()
        {
        }
    }
}