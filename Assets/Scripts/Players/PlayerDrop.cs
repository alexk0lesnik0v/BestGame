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
       
        public void OnInteract(InputValue inputValue)
        {
            if (m_objectGrabbable == null)
            {
                m_weapon.SetActive(false);
                m_handLamp.SetActive(false);
                if (Physics.Raycast(m_playerCameraTransform.position, m_playerCameraTransform.forward,
                        out RaycastHit hit, m_pickUpDistance, m_pickUpLayerMask))
                {
                    if (hit.transform.TryGetComponent(out m_objectGrabbable))
                    {
                        m_objectGrabbable.Grab(m_objectGrabPointTransform);
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