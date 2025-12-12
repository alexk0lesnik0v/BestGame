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

        private void Update()
        {
            
        }
        
        public void OnInteract(InputValue inputValue)
        {
           if (Physics.Raycast(m_playerCameraTransform.position, m_playerCameraTransform.forward,
                   out RaycastHit hit, m_pickUpDistance, m_pickUpLayerMask))
           {
               if (hit.transform.TryGetComponent(out ObjectGrabbable objectGrabbable))
               {
                   objectGrabbable.Grab(m_objectGrabPointTransform);
               }
            }
        }
    }
}