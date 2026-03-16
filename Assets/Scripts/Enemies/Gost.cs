using Players;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Gost : MonoBehaviour
    {
        [SerializeField] private Interactor m_interactor;
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_audioClip;
        
        private Player m_player;
        private NavMeshAgent m_agent;
        
        private bool m_isActive = false;
        
        private void Start()
        {
            m_player  = FindAnyObjectByType<Player>();
            m_agent = GetComponent<NavMeshAgent>();
            m_interactor.OnGostView += OnGostActivate;
        }

        private void Update()
        {
            if (m_isActive)
            {
                m_agent.SetDestination(m_player.transform.position);
                m_agent.speed = 1000f;
            }
        }

        private void OnGostActivate()
        {
            m_audioSource.PlayOneShot(m_audioClip);
            m_isActive = true;
            
            m_interactor.OnGostView -= OnGostActivate;
            
            Destroy(this.gameObject, 2f);
        }
    }
}