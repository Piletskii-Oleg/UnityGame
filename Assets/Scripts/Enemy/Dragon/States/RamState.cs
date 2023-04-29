using DG.Tweening;
using UnityEngine;

namespace Enemy.Dragon.States
{
    public class RamState : DragonBaseState
    {
        private static readonly int doFly = Animator.StringToHash("DoFly");

        private readonly Transform dragonTransform;
        
        public RamState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
            dragonTransform = dragon.transform;
        }

        public override void Enter()
        {
            dragon.SetAnimationValue(doFly, true);
            
            var position = dragonTransform.position;
            var resultPoint = position + 2 * (dragon.PlayerPosition - position);
            resultPoint.y -= 15;

            var center = dragon.Center.position;
            DOTween.Sequence()
                .Append(dragonTransform.DOMoveY(position.y - 15, 0.4f))
                .Append(dragonTransform
                        .DOMove(resultPoint, dragon.DistanceTo(resultPoint) / 20 / dragon.RamSpeed))
                .Append(dragonTransform.DOLookAt(center, 0.9f))
                .Append(dragonTransform.DOMove(center, 4f))
                .OnKill(() => stateMachine.ChangeState(dragon.SitOnGroundState));
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
            dragon.SetAnimationValue(doFly, false);
        }
    }
}