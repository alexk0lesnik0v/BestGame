using Guns;
using Players;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMonsterController1:  MonoBehaviour
    {
        [SerializeField] private Transform m_targetPosition;

        private NavMeshAgent m_agent;
        private Animator m_animator;
        
        private void Start()
        {
            m_agent = GetComponent<NavMeshAgent>();
            m_animator = GetComponent<Animator>();
        }
      
        private void Update()
        {
            if (this.gameObject.activeSelf)
            {
                m_animator.SetBool("isRunning", false);
                m_animator.Play("Walk");
                m_agent.SetDestination(m_targetPosition.position);
                m_agent.speed = 0.5f;
            }
        }
        
        private void OnDeath()
        {
        }
    }
}