using System;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public event Action Death;
        public float m_health;

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
    }
}