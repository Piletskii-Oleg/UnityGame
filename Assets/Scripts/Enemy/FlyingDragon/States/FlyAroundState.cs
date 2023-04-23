using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.FlyingDragon.States
{
    public class FlyAroundState : FlyingBaseState
    {
        private readonly Vector3[] highPoints;
        private readonly Vector3[] standPoints;

        private readonly Transform dragonTransform;

        private List<Vector3> nextPoints;

        public FlyAroundState(FlyingDragon dragon, BaseStateMachine stateMachine,
            IEnumerable<Transform> highPoints, IEnumerable<Transform> standPoints)
            : base(dragon, stateMachine)
        {
            nextPoints = new List<Vector3>();

            this.highPoints = highPoints.Select(point => point.position).ToArray();
            this.standPoints = standPoints.Select(point => point.position).ToArray();

            dragonTransform = dragon.transform;
        }

        public override void Enter()
        {
            GetNextPoints();
            
            StartFlying();
        }

        private void StartFlying()
        {
            float distance = CountAverageDistance();

            float pathDuration = distance / dragon.Speed;
            
            var standPoint = standPoints[Random.Range(0, standPoints.Length)];

            float landDuration = (nextPoints[^1] - standPoint).magnitude / dragon.Speed;
                                 
            DOTween.Sequence()
                .Append(
                    dragonTransform
                        .DOPath(nextPoints.ToArray(), pathDuration, PathType.CatmullRom)
                        .SetEase(Ease.Linear)
                        .OnWaypointChange(i =>
                        {
                            if (i < nextPoints.Count)
                            {
                                dragon.transform.DOLookAt(nextPoints[i], 1.4f);
                            }
                        }))
                .Append(dragonTransform.DOLookAt(standPoint, 1.3f))
                .Append(dragonTransform.DOMove(standPoint, landDuration))
                .OnKill(() => stateMachine.ChangeState(dragon.LandingState));
        }

        private float CountAverageDistance()
        {
            float distance = (dragonTransform.position - nextPoints[0]).magnitude;
            for (int i = 1; i < nextPoints.Count - 1; i++)
            {
                distance = distance * i / (i + 1) +
                           (nextPoints[i] - nextPoints[i + 1]).magnitude / (i + 1);
            }

            return distance;
        }

        private void GetNextPoints()
        {
            int pointCount = Random.Range(4, highPoints.Length);
            
            nextPoints.Clear();
            
            for (int i = 0; i < pointCount; i++)
            {
                nextPoints.Add(highPoints[Random.Range(0, highPoints.Length)]);
            }

            nextPoints = nextPoints.Distinct().ToList();
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
        }
    }
}