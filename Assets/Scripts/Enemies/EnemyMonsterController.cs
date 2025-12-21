using Guns;
using Players;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyMonsterController:  MonoBehaviour

    {
        [SerializeField] private int m_health = 2;
        
        private Player m_player;
        private NavMeshAgent m_agent;
        private Animator m_animator;
        private bool m_playerDetected = false;
      
        void Start()
        {
            m_player  = FindObjectOfType<Player>();
            m_agent = GetComponent<NavMeshAgent>();
            m_animator = GetComponent<Animator>();
        }
        
        private void Update()
        {
            if (m_health <= 0)
            {
                m_agent.isStopped = true;
                m_animator.Play("Dead");
                Destroy(this.gameObject.GetComponent<Collider>());
            }
            
            View();

            if (m_playerDetected && m_health > 0)
            {
                m_agent.SetDestination(m_player.transform.position);
            }
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var character))
            {
                m_animator.SetBool("isAttack", true);
                m_agent.isStopped = true;
            }
            
            if(other.gameObject.TryGetComponent<Bullet>(out var bullet))
            {
                m_health -= 1;
                Debug.Log("Critical hit!");
            }
        }

        public void OnTriggerExit(Collider other)
        {
            m_animator.SetBool("isAttack", false);
            m_agent.isStopped = false;
        }

        private void View()
        {
            if (m_health > 0)
            {
                RaycastHit hit;
                float radius = 10f;

                if (Physics.SphereCast(this.transform.position, radius, this.transform.forward, out hit, 100f))
                {
                    Debug.Log(hit.transform.name);
                    if (hit.transform.gameObject.TryGetComponent<Player>(out var player))
                    {
                        m_playerDetected =  true;
                    }
                }
            }
        }
    }
}