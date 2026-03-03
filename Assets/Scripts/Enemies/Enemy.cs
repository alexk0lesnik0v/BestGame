using System;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public event Action PlayerDetected;
        public event Action Death;

        [SerializeField] private EnemyTrigger m_enemyTrigger;
        
        public float m_health;

        private void Start()
        {
            if (m_enemyTrigger is not null)
            {
                m_enemyTrigger.PlayerTriggered += OnPlayerTriggered;
            }
        }

        public void TakeDamage(float damage)
        {
            m_health -= damage;
        }

        private void Update()
        {
            if (m_health <= 0)
            {
                Death?.Invoke();
            }
        }
        
        private void OnPlayerTriggered()
        {
            PlayerDetected?.Invoke();
            
            m_enemyTrigger.PlayerTriggered -= OnPlayerTriggered;
        }
    }
}