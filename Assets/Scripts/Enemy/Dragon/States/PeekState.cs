using DG.Tweening;
using UnityEngine;

namespace Enemy.Dragon.States
{
    public class PeekState : DragonBaseState
    {
        private static readonly int doFly = Animator.StringToHash("DoFly");

        private readonly Vector3 pointInSky;
        private readonly Transform dragonTransform;
        
        public PeekState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
            dragonTransform = dragon.transform;
        }

        public override void Enter()
        {
            DOTween.Sequence()
                .Append(dragonTransform.DOLookAt(pointInSky, 0.9f))
                .Append(dragonTransform.DOMove(pointInSky,
                    dragon.DistanceTo(dragon.PlayerPosition) / dragon.FlySpeed))
                .Append(dragonTransform.DOLookAt(dragon.PlayerPosition, 0.9f))
                .Append(dragonTransform.DOMove(dragon.PlayerPosition,
                    dragon.DistanceTo(dragon.PlayerPosition) / dragon.PeekSpeed)
                    .SetEase(Ease.InQuad)
                    .OnKill(() => dragon.SmashGround()))
                .Append(dragonTransform.DOLookAt(dragon.PlayerPosition, 0.5f))
                .OnKill(() => stateMachine.ChangeState(dragon.SitOnGroundState));
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
        }
    }
}