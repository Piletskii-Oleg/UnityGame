using DG.Tweening;
using UnityEngine;

namespace Enemy.Dragon.States
{
    public class RamState : DragonBaseState
    {
        private static readonly int doFly = Animator.StringToHash("DoFly");
        private static readonly int glide = Animator.StringToHash("Glide");

        private readonly Transform dragonTransform;
        
        public RamState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
            dragonTransform = dragon.transform;
        }

        public override void Enter()
        {
            dragon.SetAnimationValue(glide, true);
            
            var position = dragonTransform.position;
            var resultPoint = position + 2 * (dragon.GetPlayerPosition() - position);
            resultPoint.y -= 15;
            
            var sitPoint = dragon.GetPointInArea();
            DOTween.Sequence()
                .Append(dragonTransform.DOMoveY(position.y - 15, 0.7f))
                .Append(dragonTransform.DOLookAt(resultPoint, 0.8f))
                .Append(dragonTransform
                        .DOMove(resultPoint, dragon.DistanceTo(resultPoint) / 10 / dragon.RamSpeed))
                .Append(dragonTransform.DOLookAt(sitPoint, 0.9f))
                .Append(dragonTransform.DOMove(sitPoint, 4f))
                .OnKill(() => stateMachine.ChangeState(dragon.SitOnGroundState));
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
            dragon.SetAnimationValue(doFly, false);
            dragon.SetAnimationValue(glide, false);
        }
    }
}