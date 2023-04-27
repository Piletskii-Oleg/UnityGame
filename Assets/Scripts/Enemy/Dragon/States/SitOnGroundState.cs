using UnityEngine;

namespace Enemy.Dragon.States
{
    public class SitOnGroundState : DragonBaseState
    {
        private static readonly int scream = Animator.StringToHash("Scream");
        private static readonly int defend = Animator.StringToHash("Defend");
        private static readonly int eruptFlames = Animator.StringToHash("EruptFlames");
        
        public SitOnGroundState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
        }

        public override void Enter()
        {
            throw new System.NotImplementedException();
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