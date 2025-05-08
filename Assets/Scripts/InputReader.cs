using UnityEngine;
using Phac.Input;
using UnityEngine.InputSystem;
using UnityEngine.Events;

using static Phac.Input.DefaultInputAction;


namespace Phac.Character
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "OpenWorld/InputReader", order = 31)]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<Vector2, bool> Look = delegate { };
        public event UnityAction<bool> Jump = delegate { };

        private DefaultInputAction m_InputActions;

        public Vector3 Direction => (Vector3)m_InputActions.Player.Move.ReadValue<Vector2>();

        void OnEnable()
        {
            if (m_InputActions == null)
            {
                m_InputActions = new DefaultInputAction();
                m_InputActions.Player.SetCallbacks(this);
            }
            m_InputActions.Player.Enable();
        }

        void OnDisable()
        {
            if (m_InputActions == null)
            {
                m_InputActions = new DefaultInputAction();
                m_InputActions.Player.SetCallbacks(this);
            }
            m_InputActions.Player.Disable();
        }

        public void OnFire(InputAction.CallbackContext context)
        {

        }

        public void OnInteract(InputAction.CallbackContext context)
        {

        }

        public void OnLook(InputAction.CallbackContext context)
        {
            Look.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move.Invoke(context.ReadValue<Vector2>());
        }

        public void OnJump(InputAction.CallbackContext context)
        {

            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Jump.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    Jump.Invoke(false);
                    break;
            }
        }

        private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";
    }
}