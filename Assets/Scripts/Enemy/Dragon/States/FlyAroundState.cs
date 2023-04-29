using DG.Tweening;
using UnityEngine;

namespace Enemy.Dragon.States
{
    public class FlyAroundState : DragonBaseState
    {
        private static readonly int doFly = Animator.StringToHash("DoFly");

        public FlyAroundState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
        }

        public override void Enter()
        {
            var points = dragon.GetPointsAround();
            
            dragon.SetAnimationValue(doFly, true);

            DOTween.Sequence()
                .Append(dragon.transform
                    .DOPath(points, points.Length * 11 / dragon.FlySpeed, PathType.CatmullRom)
                    .OnWaypointChange(i =>
                    {
                        if (i < points.Length)
                        {
                            dragon.transform.DOLookAt(points[i], 0.5f);
                        }
                    })
                    .SetEase(Ease.InOutSine))
                .Append(dragon.transform.DOLookAt(dragon.PlayerPosition, 0.7f))
                .OnKill(() => stateMachine.ChangeState(dragon.RamState));
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
        }
    }
}