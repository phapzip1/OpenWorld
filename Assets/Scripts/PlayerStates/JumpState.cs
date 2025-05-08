using UnityEngine;

namespace Phac.Character.Player
{
    public class JumpState : BaseState
    {
        public JumpState(PlayerController controller, Animator animator) : base(controller, animator)
        {
        }

        public override void OnEnter()
        {
        }
        public override void Update()
        {
            PlayerController.ApplyGravity();
            PlayerController.HandleMoveAndRotation();
        }
    }
}