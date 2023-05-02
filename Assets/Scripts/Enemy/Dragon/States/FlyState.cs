using UnityEngine;

namespace Enemy.Dragon.States
{
    public class FlyState : DragonBaseState
    {
        private static readonly int doFly = Animator.StringToHash("DoFly");
        private static readonly int eruptFlames = Animator.StringToHash("EruptFlames");

        private readonly Vector3[] points;
        
        public FlyState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
            dragon.AddState(this);
        }

        public override void Enter()
        {
            dragon.SetAnimationValue(doFly, true);
            stateMachine.ChangeState(dragon.FlyAroundState);
        }

        public override void Tick()
        {
            
        }

        public override void Exit()
        {
            
        }

        public override void KillSequences()
        {
        }
    }
}