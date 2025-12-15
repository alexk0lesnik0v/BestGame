using UnityEngine;

namespace PickUps
{
    public class ObjectGrabbable : MonoBehaviour
    {
        private Rigidbody m_objectRigidBody;
        private Transform m_objectGrabPointTransform;

        private void Awake()
        {
            m_objectRigidBody = GetComponent<Rigidbody>();
        }
        public void Grab(Transform objectGrabPointTransform)
        {
            this.m_objectGrabPointTransform = objectGrabPointTransform;
            m_objectRigidBody.useGravity = false;
            m_objectRigidBody.freezeRotation = true;
        }

        public void Drop()
        {
            this.m_objectGrabPointTransform = null;
            m_objectRigidBody.useGravity = true;
        }

        private void Update()  
        {
            if (m_objectGrabPointTransform is not null)
            {
                float m_lerpSpeed = 100f;
                Vector3 newPosition = Vector3.Lerp(transform.position, m_objectGrabPointTransform.position, Time.deltaTime * m_lerpSpeed);
                m_objectRigidBody.MovePosition(m_objectGrabPointTransform.position);
            }
        }
    }
}