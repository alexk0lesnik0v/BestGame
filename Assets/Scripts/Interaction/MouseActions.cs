using UnityEngine;
using UnityEngine.Events;

namespace Interaction
{
    public class MouseActions : MonoBehaviour
    {
        public UnityEvent m_enter;
        public UnityEvent m_exit;
        
        private void OnMouseEnter()
        {
            m_enter?.Invoke();
        }

        private void OnMouseExit()
        {
            m_exit?.Invoke();
        }
    }
}