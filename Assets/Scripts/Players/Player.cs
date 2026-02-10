using System;
using UnityEngine;

namespace Players
{
    public class Player  : MonoBehaviour
    {
        [SerializeField] private AudioSource m_source;
        [SerializeField] private AudioClip m_clip;
        private float m_health = 50;
        
        public float health
        {
            get => m_health;
            private set
            {
                if (Mathf.Approximately(m_health, health))
                {
                    return;
                }
                
                m_health = health < 0 ? 0 : health;
                
                if (m_health is 0)
                {
                    //Died?.Invoke();
                }
            }
        }
        
        public void Heal(float heal)
        {
            if (heal < 0)
                throw new ArgumentOutOfRangeException(nameof(heal), heal,"Heal cannot be negative");
            
            m_health += heal;
        }
      
        public void TakeDamage(float damage)
        {
            m_health -= damage;
            m_source.PlayOneShot(m_clip);
        }
    }
}