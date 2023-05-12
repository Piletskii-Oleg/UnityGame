using DG.Tweening;
using UnityEngine;

namespace Enemy.Dragon.States
{
    public class RamState : DragonBaseState
    {
        private static readonly LayerMask groundMask = 1 << LayerMask.NameToLayer("Ground");
        private static readonly int doFly = Animator.StringToHash("DoFly");
        private static readonly int glide = Animator.StringToHash("Glide");

        private readonly Transform dragonTransform;

        private readonly GameObject ramCollider;
        
        private Sequence sequence;
        
        public RamState(BaseStateMachine stateMachine, Dragon dragon, GameObject ramCollider)
            : base(stateMachine, dragon)
        {
            dragonTransform = dragon.transform;
            
            dragon.AddState(this);
            
            this.ramCollider = ramCollider;
        }

        public override void Enter()
        {
            ramCollider.SetActive(true);
            
            dragon.SetAnimationValue(glide, true);
            
            var position = dragonTransform.position;
            var resultPoint = position + 2 * (dragon.GetPlayerPosition() - position);
            resultPoint.y -= 15;
            
            if (Physics.Linecast(position, resultPoint, out var info, groundMask))
            {
                resultPoint = info.transform.position;
            }

            var sitPoint = dragon.GetPointInArea();
            sequence = DOTween.Sequence()
                .Append(dragonTransform.DOMoveY(position.y - 15, 0.7f))
                .Append(dragonTransform.DOLookAt(resultPoint, 0.8f))
                .Append(dragonTransform
                        .DOMove(resultPoint, dragon.DistanceTo(resultPoint) / dragon.RamSpeed)
                        .SetEase(Ease.OutCubic))
                .Append(dragonTransform.DOLookAt(sitPoint, 0.9f))
                .Append(dragonTransform.DOMove(sitPoint, 2.1f))
                .Append(dragonTransform.DOLookAt(dragon.GetPlayerPosition(), 0.6f))
                .OnKill(() =>
                {
                    stateMachine.ChangeState(Random.Range(0.0f, 10.0f) >= 3 ? dragon.SitOnGroundState : dragon.PeekState);
                });
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
            ramCollider.SetActive(false);
            
            dragon.SetAnimationValue(doFly, false);
            dragon.SetAnimationValue(glide, false);
        }

        public override void KillSequences()
        {
            sequence.Kill();
        }
    }
}