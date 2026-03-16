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
            m_objectRigidBody.isKinematic = true;
            
            this.transform.parent = m_objectGrabPointTransform;
            this.transform.localPosition = Vector3.zero;
            this.transform.localEulerAngles = new Vector3(-105f, 0f, 0f);
            this.gameObject.layer = 9;
        }

        public void Drop()
        {
            this.m_objectGrabPointTransform = null;
            this.transform.parent = null;
            m_objectRigidBody.isKinematic = false;
            this.gameObject.layer = 0;
        }
    }
}