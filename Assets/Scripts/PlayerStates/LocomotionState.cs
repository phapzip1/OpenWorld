using UnityEngine;

namespace Phac.Character.Player
{
    public class LocomotionState : BaseState
    {
        public LocomotionState(PlayerController controller, Animator animator) : base(controller, animator)
        {
        }

        public override void OnEnter()
        {
        }

        public override void Update()
        {
            PlayerController.ApplyGravity();
            PlayerController.HandleJump();
            PlayerController.HandleMoveAndRotation();
        }
    }
}