using Enemy.Dragon.Fire;
using Enemy.Dragon.States;

namespace Enemy.Dragon
{
    public class DragonStateMachine : BaseStateMachine
    {
        private readonly FireController fireController;

        public DragonStateMachine(FireController fireController)
        {
            this.fireController = fireController;
        }

        public override void ChangeState(BaseState newState)
        {
            fireController.StopEruptingFlames();

            base.ChangeState(newState);
        }
    }
}