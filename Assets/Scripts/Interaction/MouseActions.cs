using Players;
using UnityEngine;
using UnityEngine.Events;

namespace Interaction
{
    public class MouseActions : MonoBehaviour
    {
        [SerializeField] private Interactor m_interactor;
        
        public UnityEvent m_enter;
        public UnityEvent m_exit;

        private void Start()
        {
            m_interactor = FindObjectOfType<Player>().GetComponent<Interactor>();
        }
        
        private void OnMouseEnter()
        {
            if (!m_interactor.m_isGrab)
            {
                if (m_enter is null) return;
                m_enter?.Invoke();
            }
        }

        private void OnMouseExit()
        {
            m_exit?.Invoke();
        }
    }
}