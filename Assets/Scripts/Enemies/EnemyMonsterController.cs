using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyMonsterController:  MonoBehaviour

    {
        [SerializeField] private int m_health = 1;
        
        [SerializeField] private Character m_player;
        private NavMeshAgent m_agent;
      
        void Start()
        {
            m_agent = GetComponent<NavMeshAgent>();
        }
        
        void Update()
        {
            m_agent.SetDestination(m_player.transform.position);

            if (m_health <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        /*public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Stone>(out var stone))
            {
                m_health--;
            }
        }
        */
    }
}