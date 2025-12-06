using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController m_characterController;
        [SerializeField] private CinemachineCamera m_cinemachineCamera;
        [SerializeField] private float m_speed = 10f;

        private Vector2 m_move;
        
        public void OnMove(InputValue inputValue)
        {
            m_move =  inputValue.Get<Vector2>();
        }

        private void Update()
        {
            m_characterController.Move((GetForward() * m_move.y + GetRight() * m_move.x) * Time.deltaTime * m_speed);
        }

        private Vector3 GetForward()
        {
            Vector3 forward = m_cinemachineCamera.transform.forward;
            forward.y = 0;
            
            return forward.normalized;
        }
        
        private Vector3 GetRight()
        {
            Vector3 right = m_cinemachineCamera.transform.right;
            right.y = 0;
            
            return right.normalized;
        }
    }
}