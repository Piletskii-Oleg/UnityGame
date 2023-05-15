using UnityEngine;

namespace Enemy.FlyingDragon.States
{
    /// <summary>
    /// State that corresponds to the dragon taking off the ground.
    /// </summary>
    public class TakeOffState : FlyingBaseState
    {
        private const float takeOffTime = 4f; // length of the TakeOff animation
        private float timePassed;
        
        public TakeOffState(FlyingDragon dragon, BaseStateMachine stateMachine)
            : base(dragon, stateMachine)
        {
        }

        public override void Enter()
        {
            timePassed = 0;
        }

        public override void Tick()
        {
            timePassed += Time.deltaTime;
            if (timePassed > takeOffTime)
            {
                int random = Random.Range(0, 2);
                if (random == 0)
                {
                    stateMachine.ChangeState(dragon.FlyToPointState);
                }
                else
                {
                    stateMachine.ChangeState(dragon.FlyAroundState);
                }
            }
        }

        public override void Exit()
        {
            
        }
    }
}