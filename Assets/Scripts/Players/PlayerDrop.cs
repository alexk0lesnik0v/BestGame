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
        
        private ObjectGrabbable m_objectGrabbable;

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
        }
    }
}