using Core.Enemy;
using UnityEngine;

namespace Functionality.Enemy.Spider.States
{
    /// <summary>
    /// State that corresponds to the spider being damaged.
    /// </summary>
    public class DamagedState : SpiderBaseState
    {
        private static readonly int damageAnimationHash = Animator.StringToHash("Damage");
        
        public DamagedState(Spider spider, BaseStateMachine stateMachine)
            : base(spider, stateMachine)
        {
        }

        public override void Enter()
        {
            spider.TriggerAnimation(damageAnimationHash);

            stateMachine.ChangeState(spider.AttackState);
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
        }
    }
}