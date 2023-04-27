using UnityEngine;

namespace Enemy.Dragon.States
{
    public class DeadState : DragonBaseState
    {
        private static readonly int die = Animator.StringToHash("Die");
        
        public DeadState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
        }

        public override void Enter()
        {
            dragon.TriggerAnimation(die);
        }

        public override void Tick()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}