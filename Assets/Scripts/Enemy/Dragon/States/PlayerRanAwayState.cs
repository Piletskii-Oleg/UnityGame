using UnityEngine;

namespace Enemy.Dragon.States
{
    public class PlayerRanAwayState : DragonBaseState
    {
        private static readonly int doFly = Animator.StringToHash("DoFly");
        
        public PlayerRanAwayState(BaseStateMachine stateMachine, Dragon dragon)
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