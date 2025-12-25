using UnityEngine;

namespace Interaction
{
    public class TestButtonPush : MonoBehaviour
    {
        [SerializeField] private GameObject m_doorLight1;
        [SerializeField] private GameObject m_doorLight2;
        
        private Vector3 m_startTransform;
        private float m_positionY;
        
        public bool m_isPushed = false;

        private void Start()
        {
            m_startTransform = this.gameObject.transform.position;
            m_doorLight1.SetActive(true);
        }
        private void Update()
        {
            m_positionY = m_startTransform.y - this.transform.position.y;
            if (m_positionY >= 0.07)
            {
                m_isPushed = true;
                
                if(!m_doorLight2.activeSelf)
                m_doorLight1.SetActive(false);
                else 
                    m_doorLight1.SetActive(true);
            }
            else 
            {
                m_isPushed = false;
                m_doorLight1.SetActive(true);
            }
        }
    }
}