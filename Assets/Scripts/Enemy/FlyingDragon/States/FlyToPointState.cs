using UnityEngine;
using DG.Tweening;

namespace Enemy.FlyingDragon.States
{
    public class FlyToPointState : FlyingBaseState
    {
        private readonly Transform[] points;
        
        public FlyToPointState(FlyingDragon dragon, BaseStateMachine stateMachine, Transform[] points)
            : base(dragon, stateMachine)
        {
            this.points = points;
        }

        public override void Enter()
        {
            var nextPoint = points[Random.Range(0, points.Length)];
            dragon.transform
                .DOMove(nextPoint.position, 10)
                .OnKill(() => stateMachine.ChangeState(dragon.LandingState));
        }

        public override void Tick()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}