using DG.Tweening;
using UnityEngine;

namespace Enemy.Dragon.States
{
    public class PlayerRanAwayState : DragonBaseState
    {
        private static readonly int doFly = Animator.StringToHash("DoFly");

        private readonly Vector3 runawayPoint;
        
        private Sequence sequence;
        
        public PlayerRanAwayState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
            dragon.AddState(this);
        }

        public override void Enter()
        {
            var transform = dragon.transform;
            
            dragon.SetAnimationValue(doFly, true);
            
            transform.DOLookAt(runawayPoint, 1.2f);

            float distance = (runawayPoint - transform.position).magnitude;
            transform
                .DOMove(runawayPoint, distance / dragon.FlySpeed)
                .SetEase(Ease.InSine)
                .OnKill(() => stateMachine.ChangeState(dragon.DeadState));
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
            dragon.SetAnimationValue(doFly, false);
        }

        public override void KillSequences()
        {
        }
    }
}