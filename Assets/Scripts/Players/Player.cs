using System;
using UnityEngine;

namespace Players
{
    public class Player  : MonoBehaviour
    {
        public event Action Died;
        private float m_health = 100;

        public float Health
        {
            get => m_health;
            private set
            {
                if (m_health is 0)
                {
                    Died?.Invoke();
                }
            }
        }

        public void TakeDamage(float damage)
        {
            m_health -= damage;
        }
    }
}