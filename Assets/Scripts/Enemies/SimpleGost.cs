using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SimpleGost : MonoBehaviour
    {
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_audioClip;
        [SerializeField] private Transform m_targetPosition;
        
        private Animator m_animator;
        
        private NavMeshAgent m_agent;

        private bool m_isPlaing = false;

        private void Start()
        {
            m_agent = GetComponent<NavMeshAgent>();
            m_animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (this.gameObject.activeSelf)
            {
                if (!m_isPlaing)
                {
                    m_audioSource.PlayOneShot(m_audioClip);
                    m_isPlaing = true;
                }
                
                m_agent.SetDestination(m_targetPosition.position);
                m_agent.speed = 4f;
                
                Destroy(this.gameObject, 4f);
            }
        }
    }
}