using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Enemy.Dragon.States
{
    public class PlayerRanAwayState : DragonBaseState
    {
        private static readonly int doFly = Animator.StringToHash("DoFly");

        private readonly Vector3[] runawayPath;
        
        private Sequence sequence;
        
        public PlayerRanAwayState(BaseStateMachine stateMachine, Dragon dragon, Transform[] runawayPath)
            : base(stateMachine, dragon)
        {
            dragon.AddState(this);

            this.runawayPath =
                runawayPath
                .Select(value => value.position)
                .Reverse()
                .ToArray();
        }

        public override void Enter()
        {
            dragon.SetAnimationValue(doFly, true);
            
            dragon.transform
                .DOPath(runawayPath, 8f, PathType.CatmullRom)
                .OnWaypointChange(i =>
                {
                    if (i < runawayPath.Length)
                    {
                        dragon.transform.DOLookAt(runawayPath[i], 0.5f);
                    }
                })
                .OnKill(() =>
                {
                    stateMachine.ChangeState(dragon.NotInFightState);
                    
                    dragon.HasBattleStarted = false;
                    
                    dragon.gameObject.SetActive(false);
                });
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