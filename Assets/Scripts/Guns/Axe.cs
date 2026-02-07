using Enemies;
using UnityEngine;

namespace Guns
{
    public class Axe : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_vzmah;
        [SerializeField] private AudioClip m_udar;
        [SerializeField] private AudioClip m_udarOnMonster;
        [SerializeField] private GameObject m_hitEffect;
        [SerializeField] private Camera m_camera;
        [SerializeField] private float m_damage = 25f;
        [SerializeField] private float m_force = 155f;
        [SerializeField] private float m_range = 10f;
        
        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_animator.Play("AxeIdle");
        }
        
        public void Attack()
        {
            m_animator.Play("AxeAttack");
            
            m_audioSource.PlayOneShot(m_vzmah);
            
            RaycastHit hit;

            if (Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out hit, m_range))
            {
                Debug.Log("I got you!!!" + hit.collider);

                GameObject impact = Instantiate(m_hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 0.1f);

                if (hit.rigidbody is not null)
                {
                    hit.rigidbody.AddForce(-hit.normal * m_force);
                    
                    if (hit.rigidbody.TryGetComponent<Enemy>(out var enemy))
                    {
                        m_audioSource.PlayOneShot(m_udarOnMonster);
                        enemy.TakeDamage(m_damage);
                    }
                    else
                    {
                        m_audioSource.PlayOneShot(m_udar);
                    }
                }
            }
        }
    }
}