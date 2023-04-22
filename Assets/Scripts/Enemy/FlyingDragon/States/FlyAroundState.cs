using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.FlyingDragon.States
{
    public class FlyAroundState : FlyingBaseState
    {
        private readonly Transform[] points;

        private List<Vector3> nextPoints; 
        
        public FlyAroundState(FlyingDragon dragon, BaseStateMachine stateMachine, Transform[] points)
            : base(dragon, stateMachine)
        {
            nextPoints = new();
            this.points = points;
        }

        public override void Enter()
        {
            GetNextPoints();
            
            float distance = (dragon.transform.position - nextPoints[0]).magnitude;
            float duration = distance / dragon.Speed;
            dragon.transform
                .DOPath(nextPoints.ToArray(), duration, PathType.CatmullRom)
                .OnWaypointChange(i => dragon.transform.DOLookAt(nextPoints[i], 1.0f));
        }

        private void GetNextPoints()
        {
            int pointCount = Random.Range(4, points.Length);
            nextPoints.Clear();
            for (int i = 0; i < pointCount; i++)
            {
                nextPoints.Add(points[Random.Range(0, points.Length)].position);
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