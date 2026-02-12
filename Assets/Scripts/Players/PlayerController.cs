using Enemies;
using Guns;
using Inventories;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Players
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Revolver m_revolver;
        [SerializeField] private Axe m_axe;
        [SerializeField] private CharacterController m_characterController;
        [SerializeField] private CinemachineCamera m_cinemachineCamera;
        [SerializeField] private float m_currentSpeed;
        [SerializeField] private float m_walkSpeed = 5f;
        [SerializeField] private float m_sprintSpeed = 10f;
        [SerializeField] private float m_jumpSpeed = 2.5f;
        [SerializeField] private float m_gravity = -9.81f;
        [SerializeField] private float m_crouch = 0.6f;
        [SerializeField] private GameObject m_deathUI;
        [SerializeField] private InventoryManager m_inventory;
        [SerializeField] private QuickslotInventory m_quickslotInventory;
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_walkingSound;
        [SerializeField] private AudioClip m_runningSound;
        [SerializeField] private AudioClip m_jumpingSound;
        
        private Vector2 m_move;
        private Vector3 m_movement;
        private bool m_isJump = false;
        private bool m_isCrouch = false;
        private bool m_isWalk = false;
        private bool m_isRun = false;
        public bool m_isNotFiring = false;
        private float m_playerHeight;
        private float m_reloadingTime = 10f;

        private void Start()
        {
            m_characterController = GetComponent<CharacterController>();
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
                m_isRun = true;
                m_currentSpeed =  m_sprintSpeed;
            }
            else
            {
                m_isRun = false;
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
            if (!m_isNotFiring)
            {
                if (m_quickslotInventory.m_activeSlot is not null)
                {
                    if (m_quickslotInventory.m_activeSlot.m_item is not null)
                    {
                        if (m_quickslotInventory.m_activeSlot.m_item.m_itemType == ItemType.Weapon)
                        {
                            if (m_quickslotInventory.m_activeSlot.m_item.m_itemName == "Revolver")
                            {
                                if (!m_inventory.m_isOpened)
                                {
                                    m_revolver.Fire();
                                }
                            }
                            else if (m_quickslotInventory.m_activeSlot.m_item.m_itemName == "Axe")
                            {
                                if (!m_inventory.m_isOpened)
                                {
                                    m_axe.Attack();
                                }
                            }
                        }
                    }
                }
            }
        }

        public void OnReloading(InputValue inputValue)
        {
            if (inputValue.Get<float>() > 0.5f)
            {
                if (!m_isNotFiring)
                {
                    if (m_quickslotInventory.m_activeSlot is not null)
                    {
                        if (m_quickslotInventory.m_activeSlot.m_item is not null)
                        {
                            if (m_quickslotInventory.m_activeSlot.m_item.m_itemType == ItemType.Weapon)
                            {
                                if (m_quickslotInventory.m_activeSlot.m_item.m_itemName == "Revolver")
                                {
                                    if (!m_inventory.m_isOpened)
                                    {
                                        m_revolver.Reloading();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        private void OnInventory(InputValue inputValue)
        {
            if (inputValue.Get<float>() > 0.5f)
            {
                m_inventory.m_isOpened = !m_inventory.m_isOpened;

                if (m_inventory.m_isOpened)
                {
                    this.enabled = false;
                    m_isNotFiring = true;
                
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    this.enabled = true;
                    m_isNotFiring = false;
                
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }

        private void OnUseInventory(InputValue inputValue)
        {
            if (inputValue.Get<float>() > 0.5f)
            {
                m_quickslotInventory.UseItem();
            }
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
        
        public void Dead()
        {
            this.enabled = false;
            m_cinemachineCamera.enabled = false;
            m_isNotFiring = true;
        }
    }
}