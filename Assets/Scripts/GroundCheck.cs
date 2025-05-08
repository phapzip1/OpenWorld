using UnityEngine;

namespace Phac.Character
{
    public class GroundChecker : MonoBehaviour
    {
        private RaycastHit m_GroundCheckHit = new();
        public RaycastHit GroundCheckHit { get => m_GroundCheckHit; }
        public float GroundDistance = 0.0f;
        public float SphereRadius = 1.0f;
        public bool IsGrounded { get; private set; }

        void Update()
        {
            IsGrounded = Physics.SphereCast(transform.position, SphereRadius, Vector3.down, out m_GroundCheckHit, GroundDistance);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + GroundDistance , transform.position.z);
            Gizmos.DrawSphere(spherePosition, SphereRadius);
        }
    }
}