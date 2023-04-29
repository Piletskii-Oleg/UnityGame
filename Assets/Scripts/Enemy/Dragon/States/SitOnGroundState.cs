﻿using UnityEngine;

namespace Enemy.Dragon.States
{
    public class SitOnGroundState : DragonBaseState
    {
        private static readonly int scream = Animator.StringToHash("Scream");
        private static readonly int defend = Animator.StringToHash("Defend");
        private static readonly int eruptFlames = Animator.StringToHash("EruptFlames");

        private const float sitTime = 5.0f;
        private float timePassed;
        
        public SitOnGroundState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
        }

        public override void Enter()
        {
            bool doEruptFlames = Random.Range(0, 2) != 0;
            if (doEruptFlames)
            {
                dragon.SetAnimationValue(eruptFlames, true);
                dragon.EruptFlamesGround();
            }
            else
            {
                dragon.SetAnimationValue(defend, true);
            }

            timePassed = 0;
        }

        public override void Tick()
        {
            timePassed += Time.deltaTime;
            if (timePassed > sitTime)
            {
                stateMachine.ChangeState(dragon.FlyState);
            }
        }

        public override void Exit()
        {
            dragon.SetAnimationValue(defend, false);
            dragon.SetAnimationValue(eruptFlames, false);
        }
    }
}