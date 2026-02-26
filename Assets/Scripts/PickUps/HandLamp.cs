using UnityEngine;

namespace PickUps
{
    public class HandLamp : MonoBehaviour
    {
        [SerializeField] private Transform m_objectGrabPointTransform;
        private Rigidbody m_objectRigidBody;
        
        private void Awake()
        {
            m_objectRigidBody = GetComponent<Rigidbody>();
            m_objectRigidBody.isKinematic = true;
            
            this.transform.parent = m_objectGrabPointTransform;
            this.transform.localPosition = Vector3.zero;
            this.transform.localEulerAngles = new Vector3(-105f, 0f, 0f);
            this.gameObject.layer = 9;
        }
        
        private void Update()  
        {
            if (m_objectGrabPointTransform is not null)
            {
                float m_lerpSpeed = 100f;
                Vector3 newPosition = Vector3.Lerp(transform.position, m_objectGrabPointTransform.position, Time.deltaTime * m_lerpSpeed);
                m_objectRigidBody.MovePosition(newPosition);
            }
        }
    }
}