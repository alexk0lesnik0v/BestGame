using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        private EnemyMonsterController  m_controller;
        private float m_health = 50;

        private void Start()
        {
            m_controller = GetComponent<EnemyMonsterController>();
        }
        
        public void TakeDamage(float damage)
        {
            m_health -= damage;
        }

        private void Update()
        {
            if (m_health <= 0)
            {
                m_controller.Died();
            }
        }
    }
}