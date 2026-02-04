using System;
using Guns;
using Inventories;
using PickUps;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    public class PlayerDrop : MonoBehaviour
    {
        [SerializeField] private Transform m_playerCameraTransform;
        [SerializeField] private Transform m_objectGrabPointTransform;
        [SerializeField] private float m_pickUpDistance = 2f;
        [SerializeField] private LayerMask m_pickUpLayerMask;
        [SerializeField] private GameObject m_weapon;
        [SerializeField] private GameObject m_handLamp;
        [SerializeField] InventoryManager m_inventoryManager;
        [SerializeField] private Revolver m_revolver;
        
        private ObjectGrabbable m_objectGrabbable;
        private Item m_item;
        
        private Camera m_mainCamera;

        private void Start()
        {
            m_mainCamera = Camera.main;
        }

        public bool IsGrabbed()
        {
            if (m_objectGrabbable is null) 
                return false;
            else 
                return true;
        }
       
        public void OnInteract(InputValue inputValue)
        {
            if (m_objectGrabbable is null)
            {
               if (Physics.Raycast(m_playerCameraTransform.position, m_playerCameraTransform.forward,
                        out RaycastHit hit, m_pickUpDistance, m_pickUpLayerMask))
                {
                    if (hit.transform.TryGetComponent(out m_objectGrabbable))
                    {
                        m_objectGrabbable.Grab(m_objectGrabPointTransform);
                        m_weapon.SetActive(false);
                        m_handLamp.SetActive(false);
                    }
                }
            }
            else
            {
                m_objectGrabbable.Drop();
                m_objectGrabbable = null;
                m_weapon.SetActive(true);
                m_handLamp.SetActive(true);
            }
            
            Ray ray = m_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit _hit;
            if (Physics.Raycast(ray, out _hit, m_pickUpDistance))
            {
                if (_hit.collider.TryGetComponent(out m_item))
                {
                    m_inventoryManager.AddItem(m_item.m_item, m_item.m_amount);
                    if (m_item.m_item.m_itemType == ItemType.Bullet)
                    {
                        m_revolver.m_bulletItemCount += 12;
                    }
                    
                    Destroy(m_item.gameObject);
                }
            }
        }
    }
}