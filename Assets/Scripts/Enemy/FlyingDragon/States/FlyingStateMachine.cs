namespace Enemy.FlyingDragon.States
{
    public class FlyingStateMachine : BaseStateMachine
    {
        public void KillSequence()
        {
            var state = CurrentState as FlyingBaseState;
            state?.KillSequences();
        }
    }
}