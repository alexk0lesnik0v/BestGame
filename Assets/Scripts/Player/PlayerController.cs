using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController m_characterController;
        [SerializeField] private CinemachineCamera m_cinemachineCamera;
        [SerializeField] private float m_currentSpeed;
        [SerializeField] private float m_walkSpeed = 5f;
        [SerializeField] private float m_sprintSpeed = 10f;

        private Vector2 m_move;

        private void Start()
        {
            m_currentSpeed = m_walkSpeed;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        public void OnMove(InputValue inputValue)
        {
            m_move =  inputValue.Get<Vector2>();
        }
        
        public void OnSprint(InputValue inputValue)
        {
            if (inputValue.Get<float>() > 0.5f)
            {
                m_currentSpeed =  m_sprintSpeed;
            }
            else
            {
                m_currentSpeed =  m_walkSpeed;
            }
        }

        private void Update()
        {
            m_characterController.Move((GetForward() * m_move.y + GetRight() * m_move.x) * Time.deltaTime * m_currentSpeed);
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