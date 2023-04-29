using UnityEngine;

namespace Enemy.Dragon.States
{
    public class AttackState : DragonBaseState
    {
        private static readonly int fire = Animator.StringToHash("EruptFlames");
        
        public AttackState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
        }

        public override void Enter()
        {
            dragon.SetAnimationValue(fire, true);
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