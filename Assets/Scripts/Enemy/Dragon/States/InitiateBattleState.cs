using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Enemy.Dragon.States
{
    public class InitiateBattleState : DragonBaseState
    {
        private static readonly int doFly = Animator.StringToHash("DoFly");
        private static readonly int glide = Animator.StringToHash("Glide");

        private readonly Vector3[] path;
        
        public InitiateBattleState(BaseStateMachine stateMachine, Dragon dragon, IEnumerable<Transform> path)
            : base(stateMachine, dragon)
        {
            this.path = path.Select(value => value.position).ToArray();
        }

        public override void Enter()
        {
            dragon.SetAnimationValue(doFly, true);
            dragon.SetAnimationValue(glide, true);
            
            dragon.transform.position = path[0];
            dragon.transform.DOLookAt(path[0], 0.1f);
            
            dragon.transform
                .DOPath(path, 8f, PathType.CatmullRom)
                .OnWaypointChange(i =>
                {
                    if (i < path.Length - 1)
                    {
                        dragon.transform.DOLookAt(path[i + 1], 0.5f);
                    }
                })
                .SetEase(Ease.OutSine)
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

        public override void KillSequences()
        {
        }
    }
}