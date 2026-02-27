using UnityEngine;

namespace PickUps
{
    public class HandLamp : MonoBehaviour
    {
        private Transform m_objectGrabPointTransform;
        private Rigidbody m_objectRigidBody;
        
        private void Awake()
        {
            m_objectRigidBody = GetComponent<Rigidbody>();
        }

        public void PickUp(Transform pickupTransform)
        {
            this.m_objectGrabPointTransform = pickupTransform;
            m_objectRigidBody.useGravity = false;
        }
        
        private void Update()  
        {
            if (m_objectGrabPointTransform is not null)
            {
                m_objectRigidBody.MovePosition(m_objectGrabPointTransform.position);
            }
        }
    }
}