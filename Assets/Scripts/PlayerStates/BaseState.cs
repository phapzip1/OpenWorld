using UnityEngine;

namespace Phac.Character.Player
{
    public abstract class BaseState : IState
    {
        protected readonly PlayerController PlayerController;
        protected readonly Animator Animator;

        protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        // protected static readonly int JumpHash = Animator.StringToHash("Jump");
        // protected static readonly int DashHash = Animator.StringToHash("Dash");
        // protected static readonly int AttackHash = Animator.StringToHash("Attack");

        protected BaseState(PlayerController controller, Animator animator) {
            PlayerController = controller;
            Animator = animator;
        }
        
        public virtual void FixedUpate()
        {
        
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }

        public virtual void Update()
        {

        }
    }
}