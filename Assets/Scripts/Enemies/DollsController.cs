using System.Collections.Generic;
using Guns;
using Players;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class DollsController : MonoBehaviour
    {
        [SerializeField] private float m_health = 75f;
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_audioClip;
        [SerializeField] private AudioClip m_deathAudioClip;
        [SerializeField] private float m_damage;
        [SerializeField] private DollParts m_deathPrefab;

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
            m_player = FindAnyObjectByType<Player>();
            m_agent = GetComponent<NavMeshAgent>();
            m_animator = GetComponent<Animator>();
            
            m_enemy.m_health = m_health;
            
            m_enemy.Death += OnDeath;
            m_enemy.PlayerDetected += OnPlayerDetected;
        }
        
        private void Update()
        {

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
            if (m_playerDetected && !m_isDead && !m_isDamage)
            {
                if (other.gameObject.TryGetComponent<Player>(out var character))
                {
                    m_agent.SetDestination(character.transform.position);
                    m_animator.SetBool("isAttack", true);
                    m_animator.Play("Attack");
                    m_isAttack = true;
                    m_agent.isStopped = true;
                    
                    if (!m_isVoice)
                    {
                        m_audioSource.PlayOneShot(m_audioClip);
                        m_isVoice = true;
                    }
                    
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
        
        private void OnPlayerDetected()
        {
            m_audioSource.PlayOneShot(m_audioClip);
            m_enemy.m_health = m_health;
            m_playerDetected = true;
            
            m_enemy.PlayerDetected -= OnPlayerDetected;
        }

        private void OnDeath()
        {
            if (!m_playerDetected)
            {
                m_enemy.m_health = m_health;
                return;
            }
            
            Transform currentTransform = this.gameObject.transform;
            Destroy(this.gameObject);
            Instantiate(m_deathPrefab, currentTransform.position + Vector3.up, currentTransform.rotation);
            m_isDamage = false;
            m_player.m_isEnemy = false;
            m_enemy.Death -= OnDeath;
        }
    }
}