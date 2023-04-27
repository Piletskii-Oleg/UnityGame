using UnityEngine;

namespace Enemy.Dragon.States
{
    public class FlyState : DragonBaseState
    {
        private static readonly int doFly = Animator.StringToHash("DoFly");
        private static readonly int eruptFlames = Animator.StringToHash("EruptFlames");
        
        public FlyState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
        }

        public override void Enter()
        {
            
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