using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Enemy.Dragon.States
{
    public class FlyAroundState : DragonBaseState
    {
        private static readonly int doFly = Animator.StringToHash("DoFly");
        private static readonly int eruptFlames = Animator.StringToHash("EruptFlames");

        public FlyAroundState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
        }

        public override void Enter()
        {
            var points = dragon.GetPointsAround();
            
            dragon.SetAnimationValue(doFly, true);
            
            bool doEruptFlames = Random.Range(0, 2) != 0;

            if (doEruptFlames)
            {
                dragon.SetAnimationValue(eruptFlames, true);
                dragon.EruptFlamesFlying();
            }

            DOTween.Sequence()
                .Append(dragon.transform
                    .DOPath(points, points.Length * 50 / dragon.FlySpeed, PathType.CatmullRom)
                    .OnWaypointChange(i =>
                    {
                        if (i < points.Length)
                        {
                            dragon.transform.DOLookAt(points[i], 0.5f);
                        }
                    })
                    .SetEase(Ease.Unset))
                .Append(dragon.transform.DOLookAt(dragon.PlayerPosition, 0.7f))
                .OnKill(() => stateMachine.ChangeState(dragon.SitOnGroundState));
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
            dragon.SetAnimationValue(doFly, false);
            dragon.SetAnimationValue(eruptFlames, false);
        }
    }
}