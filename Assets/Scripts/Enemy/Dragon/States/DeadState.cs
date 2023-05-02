using DG.Tweening;
using UnityEngine;

namespace Enemy.Dragon.States
{
    public class DeadState : DragonBaseState
    {
        private static readonly int doFly = Animator.StringToHash("DoFly");
        private static readonly int die = Animator.StringToHash("Die");

        private Sequence sequence;
        
        public DeadState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
            dragon.AddState(this);
        }

        public override void Enter()
        {
            dragon.SetAnimationValue(doFly, true);
            var position = dragon.Center.position;

            sequence = DOTween.Sequence()
                .Append(dragon.transform.DOMove(position, dragon.DistanceTo(position) / dragon.FlySpeed))
                .OnComplete(Die);
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

        private void Die()
        {
            dragon.SetAnimationValue(doFly, false);
            dragon.TriggerAnimation(die);
        }
    }
}