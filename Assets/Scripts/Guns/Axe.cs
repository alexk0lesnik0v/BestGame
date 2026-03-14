using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private List<GameObject> m_hitEffectEnemy;
        [SerializeField] private GameObject m_hitEffect;
        [SerializeField] private Camera m_camera;
        [SerializeField] private float m_damage = 25f;
        [SerializeField] private float m_force = 155f;
        [SerializeField] private float m_range = 3f;
        
        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_animator.Play("AxeOnHand");
        }
        
        public void Attack()
        {
            m_animator.Play("AxeAttack");
            
            m_audioSource.PlayOneShot(m_vzmah);
            
            StartCoroutine(WaitAttack(0.3f));
        }
        
        IEnumerator WaitAttack(float time)
        {
            yield return new WaitForSeconds(time);
            
            RaycastHit hit;
            
            if (Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out hit, m_range))
            {
                Debug.Log("I got you!!!" + hit.collider);
                
                if (hit.rigidbody is not null)
                {
                    hit.rigidbody.AddForce(-hit.normal * m_force);
                    
                    if (hit.rigidbody.TryGetComponent<Enemy>(out var enemy))
                    {
                        if (m_hitEffectEnemy.Count > 0)
                        {
                            int index = Random.Range(0, m_hitEffectEnemy.Count);
                            GameObject effect = m_hitEffectEnemy[index];
                    
                            GameObject impact = Instantiate(effect, hit.point, Quaternion.LookRotation(hit.normal));
                            Destroy(impact.GetComponent<Collider>(), 0.1f);
                            Destroy(impact, 60f);
                        }
                        
                        m_audioSource.PlayOneShot(m_udarOnMonster);
                        enemy.TakeDamage(m_damage);
                    }
                    else
                    {
                        GameObject impact = Instantiate(m_hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(impact.GetComponent<Collider>(), 0.1f);
                        Destroy(impact, 60f);
                        m_audioSource.PlayOneShot(m_udar);
                    }
                }
                else
                {
                    GameObject impact = Instantiate(m_hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impact.GetComponent<Collider>(), 0.1f);
                    Destroy(impact, 60f);
                    m_audioSource.PlayOneShot(m_udar);
                }
            }
        }

        public void AxeOnHand()
        {
            m_animator.Play("AxeOnHand");
        }
    }
}