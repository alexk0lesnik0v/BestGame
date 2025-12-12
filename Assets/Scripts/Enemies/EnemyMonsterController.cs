using Players;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyMonsterController:  MonoBehaviour

    {
        [SerializeField] private int m_health = 1;
        [SerializeField] private GameObject m_bullet;
        
        private Player m_player;
        private NavMeshAgent m_agent;
        private Animator m_animator;
      
        void Start()
        {
            m_player  = FindObjectOfType<Player>();
            m_agent = GetComponent<NavMeshAgent>();
            m_animator = GetComponent<Animator>();
        }
        
        void Update()
        {
            m_agent.SetDestination(m_player.transform.position);
            
            if (m_health <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var character))
            {
                m_animator.SetBool("isAttack", true);
            }
            else if (other == m_bullet)
            {
                m_animator.SetBool("Hit", true);
            }
            else
            {
                m_animator.SetBool("isAttack", false);
                m_animator.SetBool("Hit", false);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            m_animator.SetBool("isAttack", false);
            m_animator.SetBool("Hit", false);
        }
    }
}