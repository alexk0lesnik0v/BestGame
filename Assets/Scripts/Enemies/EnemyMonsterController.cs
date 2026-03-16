using Guns;
using Players;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMonsterController:  MonoBehaviour
    {
        [SerializeField] private float m_health = 75f;
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_audioClip;
        [SerializeField] private float m_damage;

        private Enemy m_enemy;
        private Player m_player;
        private NavMeshAgent m_agent;
        private Animator m_animator;
        private bool m_playerDetected = false;
        private bool m_isAttack  = false;
        private bool m_isDead = false;
        private bool m_isVoice = false;
        private bool m_isDamage = false;
        
        private void Start()
        {
            m_enemy = GetComponent<Enemy>();
            m_player  = FindAnyObjectByType<Player>();
            m_agent = GetComponent<NavMeshAgent>();
            m_animator = GetComponent<Animator>();
            
            m_enemy.m_health = m_health;
            
            m_enemy.Death += OnDeath;
            m_enemy.PlayerDetected += OnPlayerDetected;
        }
      
        private void Update()
        {
            View();

            if (m_playerDetected && !m_isDead && !m_isAttack)
            {
                m_agent.SetDestination(m_player.transform.position);
                m_animator.SetBool("isRunning", true);
                m_animator.Play("Run");
                m_agent.speed = 4;
            }
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if (!m_isDead && !m_isDamage)
            {
                if (other.gameObject.TryGetComponent<Player>(out var character))
                {
                    m_agent.SetDestination(character.transform.position);
                    m_animator.SetBool("isAttack", true);
                    m_animator.Play("Attack");
                    m_isAttack = true;
                    m_agent.isStopped = true;
                    m_isDamage = true;
                    character.m_isEnemy = true;
                    character.TakeDamage(m_damage);
                }
            
                if (other.gameObject.TryGetComponent<Bullet>(out var bullet))
                {
                    if (!m_playerDetected)
                    {
                        m_audioSource.PlayOneShot(m_audioClip);
                    }
                    
                    m_playerDetected =  true;
                    Debug.Log("Critical hit!");
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var player))
            {
                m_animator.SetBool("isAttack", false);
                m_isAttack  = false;
                m_agent.isStopped = false;
                m_isDamage = false;
                player.m_isEnemy = false;
            }
        }

        private void View()
        {
            if (!m_isDead)
            {
                RaycastHit hit;
                float radius = 3f;

                if (Physics.SphereCast(this.transform.position, radius, this.transform.forward, out hit, 10f))
                {
                    if (hit.transform.gameObject.TryGetComponent<Player>(out var player))
                    {
                        if (!m_isVoice)
                        {
                            m_audioSource.PlayOneShot(m_audioClip);
                            m_isVoice = true;
                        }
                        
                        m_playerDetected = true;
                    }
                }
            }
        }
        
        private void OnPlayerDetected()
        {
            m_audioSource.PlayOneShot(m_audioClip);
            m_playerDetected = true;
            
            m_enemy.PlayerDetected -= OnPlayerDetected;
        }

        private void OnDeath()
        {
            m_isDead = true;
            m_isAttack  = false;
            m_agent.isStopped = true;
            m_isDamage = false;
            m_player.m_isEnemy = false;
            m_animator.Play("Death");
            
            Collider[] colliders = this.GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders)
            {
                Destroy(col);
            }
            
            this.enabled = false;
            m_enemy.Death -= OnDeath;
        }
    }
}