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
            //m_objectRigidBody.useGravity = false;
            //m_objectRigidBody.freezeRotation = false;
            m_objectRigidBody.isKinematic = true;
            
            this.transform.parent = m_objectGrabPointTransform;
            this.transform.localPosition = Vector3.zero;
            this.transform.localEulerAngles = new Vector3(-105f, 0f, 0f);
        }

        public void Drop()
        {
            this.m_objectGrabPointTransform = null;
            this.transform.parent = null;
            //m_objectRigidBody.useGravity = true;
            m_objectRigidBody.isKinematic = false;
        }

        private void Update()  
        {
            /*if (m_objectGrabPointTransform is not null)
            {
                float m_lerpSpeed = 100f;
                Vector3 newPosition = Vector3.Lerp(transform.position, m_objectGrabPointTransform.position, Time.deltaTime * m_lerpSpeed);
                m_objectRigidBody.MovePosition(m_objectGrabPointTransform.position);
            }*/
        }
    }
}