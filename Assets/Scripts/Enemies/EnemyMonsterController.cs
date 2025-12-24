using System.Collections.Generic;
using Guns;
using Players;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyMonsterController:  MonoBehaviour

    {
        [SerializeField] private int m_health = 2;
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_audioClip;
        [SerializeField] private Transform m_patrolRoute;
        [SerializeField] private List<Transform> m_locations;
        
        private Player m_player;
        private NavMeshAgent m_agent;
        private Animator m_animator;
        private bool m_playerDetected = false;
        private bool m_isAttack  = false;
        private int m_locationIndex = 0;
        
        void Start()
        {
            m_player  = FindObjectOfType<Player>();
            m_agent = GetComponent<NavMeshAgent>();
            m_animator = GetComponent<Animator>();
            
            InitializePatrolRoute();

            MoveToNextPatrolLocation();
        }
        
        private void Update()
        {
            if (m_health <= 0)
            {
                m_agent.isStopped = true;
                m_animator.Play("Death");
                Destroy(this.gameObject.GetComponent<Collider>());
            }
            
            View();
            
            if(m_agent.remainingDistance < 0.2f && !m_agent.pathPending)
            {
                MoveToNextPatrolLocation();
            }

            if (m_playerDetected && m_health > 0 && !m_isAttack)
            {
                m_agent.SetDestination(m_player.transform.position);
                m_animator.SetBool("isRunning", true);
                m_animator.Play("Run");
                m_agent.speed = 4;
                m_audioSource.PlayOneShot(m_audioClip);
            }
        }
        
        void InitializePatrolRoute()
        {
            foreach (Transform child in m_patrolRoute)
            {
                m_locations.Add(child);
            }
        }

        void MoveToNextPatrolLocation()
        { 
            if(m_locations.Count == 0)
                return;

            m_agent.destination = m_locations[m_locationIndex].position;

            m_locationIndex = (m_locationIndex + 1) % m_locations.Count;
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var character))
            {
                m_agent.SetDestination(character.transform.position);
                m_animator.SetBool("isAttack", true);
                m_animator.Play("Attack");
                m_audioSource.PlayOneShot(m_audioClip);
                m_isAttack = true;
                m_agent.isStopped = true;
            }
            
            if(other.gameObject.TryGetComponent<Bullet>(out var bullet))
            {
                m_health -= 1;
                m_playerDetected =  true;
                Debug.Log("Critical hit!");
            }
        }

        public void OnTriggerExit(Collider other)
        {
            m_animator.SetBool("isAttack", false);
            m_isAttack  = false;
            m_agent.isStopped = false;
        }

        private void View()
        {
            if (m_health > 0)
            {
                RaycastHit hit;
                float radius = 3f;

                if (Physics.SphereCast(this.transform.position, radius, this.transform.forward, out hit, 10f))
                {
                    //Debug.Log(hit.transform.name);
                    if (hit.transform.gameObject.TryGetComponent<Player>(out var player))
                    {
                        m_playerDetected =  true;
                    }
                }
            }
        }
    }
}