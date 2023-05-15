using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

namespace Enemy.FlyingDragon.States
{
    /// <summary>
    /// State that corresponds to the dragon flying to a randomly chosen point on the map.
    /// </summary>
    public class FlyToPointState : FlyingBaseState
    {
        private readonly Vector3[] points;

        private int currentPointNumber;

        private Sequence sequence;
        
        public FlyToPointState(FlyingDragon dragon, BaseStateMachine stateMachine, IEnumerable<Transform> points)
            : base(dragon, stateMachine)
        {
            this.points = points.Select(point => point.position).ToArray();
            currentPointNumber = -1;
        }

        public override void Enter()
        {
            var nextPoint = DetermineNextPoint();
            float distance = (dragon.transform.position - nextPoint).magnitude;
            float duration = distance / dragon.Speed;

            sequence = DOTween.Sequence()
                .Append(dragon.transform.DOLookAt(nextPoint, 0.3f))
                .Append(dragon.transform.DOMove(nextPoint, duration).SetEase(Ease.InOutSine))
                .OnKill(() => stateMachine.ChangeState(dragon.LandingState));
        }

        private Vector3 DetermineNextPoint()
        {
            int nextPointNumber = Random.Range(0, points.Length);
            while (nextPointNumber == currentPointNumber)
            {
                nextPointNumber = Random.Range(0, points.Length);
            }

            currentPointNumber = nextPointNumber;
            return points[nextPointNumber];
        }

        public override void Tick()
        {
            
        }

        public override void Exit()
        {
            
        }

        public override void KillSequences()
        {
            sequence.Kill();
        }
    }
}