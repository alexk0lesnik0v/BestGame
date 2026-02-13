using System;
using DefaultNamespace.UI;
using UnityEngine;

namespace Players
{
    public class Player  : MonoBehaviour
    {
        [SerializeField] private AudioSource m_source;
        [SerializeField] private AudioClip m_clip;
        [SerializeField] private DeathUI m_deathUI;
        private float m_health = 100;
        
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
            }
        }
        
        public void Heal(float heal)
        {
            if (heal < 0)
                throw new ArgumentOutOfRangeException(nameof(heal), heal,"Heal cannot be negative");
            
            m_health += heal;
            
            m_deathUI.SetImage(m_health);
        }
      
        public void TakeDamage(float damage)
        {
            m_health -= damage;
            m_source.PlayOneShot(m_clip);
            
            m_deathUI.SetImage(m_health);
        }
    }
}