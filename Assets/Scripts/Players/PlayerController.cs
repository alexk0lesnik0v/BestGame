using Guns;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Revolver m_revolver;
        [SerializeField] private CharacterController m_characterController;
        [SerializeField] private CinemachineCamera m_cinemachineCamera;
        [SerializeField] private float m_currentSpeed;
        [SerializeField] private float m_walkSpeed = 5f;
        [SerializeField] private float m_sprintSpeed = 10f;
        [SerializeField] private float m_jumpSpeed = 2.5f;
        [SerializeField] private float m_gravity = -9.81f;
        [SerializeField] private float m_crouch = 0.6f;
        
        private Vector2 m_move;
        private Vector3 m_movement;
        private bool m_isJump = false;
        private bool m_isCrouch = false;
        private float m_playerHeight;

        private void Start()
        {
            m_characterController =  GetComponent<CharacterController>();
            m_currentSpeed = m_walkSpeed;
            m_playerHeight = m_characterController.height;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
       
        public void OnMove(InputValue inputValue)
        {
            m_move =  inputValue.Get<Vector2>();
            m_movement.x = m_move.x;
            m_movement.z = m_move.y;
        }
        
        public void OnSprint(InputValue inputValue)
        {
            if (inputValue.Get<float>() > 0.5f && !m_isCrouch)
            {
                m_currentSpeed =  m_sprintSpeed;
            }
            else
            {
                m_currentSpeed =  m_walkSpeed;
            }
        }

        public void OnJump(InputValue inputValue)
        {
            if (m_characterController.isGrounded && !m_isJump) m_isJump = true;
        }

        public void OnCrouch(InputValue inputValue)
        {
            if (inputValue.Get<float>() > 0.5f)
            {
                m_isCrouch = true;
                m_characterController.height = m_playerHeight * m_crouch;
            }
            else
            {
                m_characterController.height = m_playerHeight;
                m_isCrouch = false;
            }
        }

        public void OnFire()
        {
            m_revolver.Fire();
        }

        private void Update()
        {
            m_characterController.Move((GetForward() * m_movement.z + GetRight() * m_movement.x + GetUp() * m_movement.y) * Time.deltaTime * m_currentSpeed);

            if (m_characterController.isGrounded)
            {
                m_movement.y = m_gravity * 0.1f;

                if (m_isJump)
                {
                    m_movement.y = m_jumpSpeed;
                    m_isJump = false;
                }
            }
            else
            {
                m_movement.y += m_gravity * Time.deltaTime;
            }
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
        
        private Vector3 GetUp()
        {
            Vector3 up = m_cinemachineCamera.transform.up;
            up.z = 0;
            up.x = 0;
            
            return up.normalized;
        }
    }
}