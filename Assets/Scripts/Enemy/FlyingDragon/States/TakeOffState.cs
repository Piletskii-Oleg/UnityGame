using UnityEngine;

namespace Enemy.FlyingDragon.States
{
    public class TakeOffState : FlyingBaseState
    {
        private const float takeOffTime = 4f;
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
                //int random = Random.Range(0, 2);
                int random = 1;
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