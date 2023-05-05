using DG.Tweening;
using UnityEngine;

namespace Enemy.Dragon.States
{
    public class FlyAroundState : DragonBaseState
    {
        private static readonly int doFly = Animator.StringToHash("DoFly");
        private static readonly int glide = Animator.StringToHash("Glide");
        private static readonly int eruptFlames = Animator.StringToHash("EruptFlames");

        private Sequence sequence;
        
        public FlyAroundState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
            dragon.AddState(this);
        }

        public override void Enter()
        {
            var points = dragon.GetPointsAround();
            
            dragon.SetAnimationValue(doFly, true);
            dragon.SetAnimationValue(glide, true);

            bool doEruptFlames = Random.Range(0, 2) == 0;
            if (doEruptFlames)
            {
                dragon.SetAnimationValue(eruptFlames, true);
            }

            sequence = DOTween.Sequence()
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
                .Append(dragon.transform.DOLookAt(dragon.GetPlayerPosition(), 0.7f))
                .OnKill(() => stateMachine.ChangeState(Random.Range(0, 2) == 0 ? dragon.PeekState : dragon.RamState));
            
        }

        public override void Tick()
        {
        }

        public override void KillSequences()
        {
            sequence.Kill();
        }

        public override void Exit()
        {
            dragon.SetAnimationValue(glide, false);
            dragon.SetAnimationValue(eruptFlames, false);
        }
    }
}