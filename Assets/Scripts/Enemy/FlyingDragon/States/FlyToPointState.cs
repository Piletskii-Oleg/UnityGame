using UnityEngine;
using DG.Tweening;

namespace Enemy.FlyingDragon.States
{
    public class FlyToPointState : FlyingBaseState
    {
        private readonly Transform[] points;

        private int currentPointNumber;
        
        public FlyToPointState(FlyingDragon dragon, BaseStateMachine stateMachine, Transform[] points)
            : base(dragon, stateMachine)
        {
            this.points = points;
            currentPointNumber = -1;
        }

        public override void Enter()
        {
            var nextPoint = DetermineNextPoint();
            float distance = (dragon.transform.position - nextPoint).magnitude;
            float duration = distance / dragon.Speed;

            DOTween.Sequence()
                .Append(dragon.transform.DOLookAt(nextPoint, 0.3f))
                .Append(dragon.transform.DOMove(nextPoint, duration).SetEase(Ease.InOutSine))
                .OnKill(() => stateMachine.ChangeState(dragon.LandingState));
        }

        private Vector3 DetermineNextPoint()
        {
            var nextPointNumber = Random.Range(0, points.Length);
            while (nextPointNumber == currentPointNumber)
            {
                nextPointNumber = Random.Range(0, points.Length);
            }

            currentPointNumber = nextPointNumber;
            var nextPoint = points[nextPointNumber].position;
            return nextPoint;
        }

        public override void Tick()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}