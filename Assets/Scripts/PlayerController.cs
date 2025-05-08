using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;



namespace Phac.Character
{
    public class PlayerController : MonoBehaviour
    {
        #region Serialize Fields
        [SerializeField] private Animator Animator;
        [SerializeField] private CinemachineCamera FreelookCamera;
        [SerializeField] private InputReader Input;
        #endregion

        #region Components
        private Transform m_MainCameraTransform;
        private GroundChecker m_GroundChecker;
        private CharacterController m_CharacterController;
        #endregion

        #region Public Variables
        [Header("Movement Settings")]
        public float MaxVelocity = 5.0f;
        public float RotationSpeed = 15.0f;
        public float SmoothTime = 0.2f;

        [Header("Jump Settings")]
        public float JumpDuration = 1.0f;
        public float JumpVelocity = 5.0f;
        public float FallMultipler = 4.0f;
        public float FallVelocityCap = -20.0f;
        #endregion

        #region Members
        private List<Utility.Timer> m_Timers;
        private Utility.CountDownTimer m_JumpTimer;
        private Vector2 m_Velocity = Vector2.zero;
        private Vector3 m_AdjustedDirection = Vector3.zero;
        #endregion

        #region Unity built-in functions
        void Awake()
        {
            RegisterComponents();

            // Setup ground check
            float sphereRadius = m_CharacterController.radius * 0.9f;
            m_GroundChecker.GroundDistance =  m_CharacterController.bounds.extents.y -  sphereRadius + 0.05f;
            m_GroundChecker.SphereRadius = sphereRadius;

            m_MainCameraTransform = FreelookCamera.transform;
            m_JumpTimer = new Utility.CountDownTimer(JumpDuration);
            m_Timers = new List<Utility.Timer>(2) { m_JumpTimer };
        }

        void Update()
        {
            Debug.Log(m_GroundChecker.IsGrounded);
            HandleTimers();
            HandleMoveAndRotation();
            ApplyGravity();
            HandleJump();

            ApplyMovement();
        }

        void OnEnable()
        {
            Input.Jump += OnJump;
        }

        void OnDisable()
        {
            Input.Jump -= OnJump;
        }

        #endregion

        #region Helpers and Callbacks
        private void RegisterComponents()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_GroundChecker = GetComponent<GroundChecker>();
        }

        private void ApplyMovement()
        {
            if (m_AdjustedDirection.magnitude > 0.0f)
            {
                Vector3 smoothRotation = Vector3.Lerp(transform.forward, m_AdjustedDirection, Time.deltaTime * RotationSpeed);
                transform.rotation = Quaternion.LookRotation(smoothRotation);
            }

            Vector3 movement = Time.deltaTime * m_Velocity.x * m_AdjustedDirection;
            movement.y = Time.deltaTime * m_Velocity.y;

            m_CharacterController.Move(movement);
        }

        private void HandleMoveAndRotation()
        {
            Vector3 direction = new Vector3(Input.Direction.x, 0.0f, Input.Direction.y).normalized;
            m_AdjustedDirection = Quaternion.AngleAxis(m_MainCameraTransform.eulerAngles.y, Vector3.up) * direction;

            m_Velocity.x = Mathf.Lerp(m_Velocity.x, direction.magnitude > 0.1f ? MaxVelocity : 0.0f, Time.deltaTime);
        }


        private void ApplyGravity()
        {
            // If grounded apply no gravity
            if (m_GroundChecker.IsGrounded)
            {
                m_Velocity.y = 0.0f;
            }
            // If the character is jumping
            else if (!m_JumpTimer.IsRunning)
            {
                m_Velocity.y += Physics.gravity.y * (m_Velocity.y < 0.0f ? FallMultipler : 1.0f) * Time.deltaTime;
                m_Velocity.y = Mathf.Max(m_Velocity.y, FallVelocityCap);
            }
        }

        private void HandleJump()
        {
            if (m_JumpTimer.IsRunning)
            {
                Debug.Log("Jump");
                m_Velocity.y = JumpVelocity;
            }
        }

        private void OnJump(bool performed)
        {
            if (performed && !m_JumpTimer.IsRunning && m_GroundChecker.IsGrounded)
            {
                m_JumpTimer.Start();

            }
            else if (!performed && m_JumpTimer.IsRunning)
            {
                m_JumpTimer.Stop();
            }
        }

        private void HandleTimers()
        {
            foreach (var timer in m_Timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }
        #endregion
    }

}