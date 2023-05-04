using DG.Tweening;
using UnityEngine;

namespace Enemy.Dragon.States
{
    public class PeekState : DragonBaseState
    {
        private static readonly int doFly = Animator.StringToHash("DoFly");
        
        private readonly Transform dragonTransform;
        
        private Sequence sequence;
        
        public PeekState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
            dragonTransform = dragon.transform;
            
            dragon.AddState(this);
        }

        public override void Enter()
        {
            var pointInSky = dragon.GetPointInArea();
            pointInSky.y += 150;
            
            sequence = DOTween.Sequence()
                .Append(dragonTransform.DOLookAt(pointInSky, 0.6f))
                .Append(dragonTransform
                    .DOMove(pointInSky, dragon.DistanceTo(dragon.GetPlayerPosition()) / dragon.FlySpeed)
                    .SetEase(Ease.OutCubic))
                .Append(dragonTransform.DOLookAt(dragon.GetPlayerPosition(), 0.6f))
                .Append(dragonTransform.DOMove(dragon.GetPlayerPosition(),
                    dragon.DistanceTo(dragon.GetPlayerPosition()) / dragon.PeekSpeed)
                    .SetEase(Ease.InCubic)
                    .OnComplete(() => dragon.SmashGround()))
                .Append(dragonTransform.DOLookAt(dragon.GetPlayerPosition(), 0.5f))
                .Append(dragonTransform.DOMoveY(dragon.BaseHeight, 0.3f))
                .OnKill(() => stateMachine.ChangeState(dragon.SitOnGroundState));
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
            sequence.Kill();
        }
    }
}