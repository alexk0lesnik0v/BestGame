using UnityEngine;

namespace Interaction
{
    public class TestButtonPush : MonoBehaviour
    {
        private Vector3 m_startTransform;
        private float m_positionY;

        private void Start()
        {
            m_startTransform = this.gameObject.transform.position;
        }
        private void Update()
        {
            m_positionY = m_startTransform.y - this.transform.position.y;
            if (m_positionY >= 0.07)
            {
                Debug.Log("Нажата");
            }
        }
    }
}