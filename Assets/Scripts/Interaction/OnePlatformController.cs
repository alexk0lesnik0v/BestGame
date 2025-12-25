using UnityEngine;

namespace Interaction
{
    public class OnePlatformController : MonoBehaviour
    {
        [SerializeField] private GameObject m_doorLight;
        
        private Vector3 m_startTransform;
        private float m_positionY;
        
        public bool m_isPushed = false;

        private void Start()
        {
            m_startTransform = this.gameObject.transform.position;
            m_doorLight.SetActive(true);
        }
        private void Update()
        {
            m_positionY = m_startTransform.y - this.transform.position.y;
            if (m_positionY >= 0.07)
            {
                m_isPushed = true;
                m_doorLight.SetActive(false);
            }
            else 
            {
                m_isPushed = false;
                m_doorLight.SetActive(true);
            }
        }
    }
}