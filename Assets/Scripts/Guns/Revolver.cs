using System;
using System.Collections;
using UnityEngine;

namespace Guns
{
    public class Revolver : MonoBehaviour
    {
        [SerializeField] private float m_damage = 21f;
        
        [SerializeField] private float m_force = 155f;

        [SerializeField] private float m_range = 100f;
        
        [SerializeField] private float m_rotateSpeed = 5f;

        [SerializeField] private ParticleSystem m_muzzleFlash;

        [SerializeField] private Transform m_bulletSpawn;

        [SerializeField] private AudioClip m_shotSFX;

        [SerializeField] private AudioSource m_audioSource;

        [SerializeField] private GameObject m_hitEffect;

        [SerializeField] private Camera m_camera;

        [SerializeField] private GameObject m_bullet;

        private float m_nextFire = 0f;
        private float m_bulletSpeed = 100f;
        private bool m_isFiring = false;
        private bool m_isReloading = false;
        
        [SerializeField] private Animator m_animator;
        
        [SerializeField] [Min(0)] public int m_bulletCount = 6;
        [Min(0)] public int m_bulletItemCount = 0;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
        }
        public void Fire()
        {
            Debug.Log("Fire");
            m_isFiring = true;
        }

        public void StopFire()
        {
            m_animator.SetBool("Shoot", false);
        }
        private void Update()
        {
            var angle = transform.eulerAngles;
            
            if (m_isFiring && !m_isReloading && m_bulletCount > 0)
            {
                Shoot();
            }
            else if (m_isFiring && !m_isReloading && m_bulletCount == 0)
            {
                m_animator.Play("Shooting");
            }
            m_isFiring = false;

            if (!m_isFiring && !m_isReloading)
            {
                m_animator.Play("PrepareForShooting");
            }
        }
        
        private void Shoot()
        {
            m_bulletCount -= 1;
            
            m_animator.Play("Shooting");
            
            m_audioSource.PlayOneShot(m_shotSFX);

            m_muzzleFlash.Play();
            
            RaycastHit hit;

            if (Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out hit, m_range))
            {
                Debug.Log("I got you!!!" + hit.collider);

                GameObject impact = Instantiate(m_hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 0.1f);

                if (hit.rigidbody is not null)
                {
                    hit.rigidbody.AddForce(-hit.normal * m_force);
                }
            }
        }

        public void Reloading()
        {
            m_isReloading = true;
            m_animator.Play("OpenReloader");

            if (m_bulletItemCount == 0)
            {
                StartCoroutine(WaitReloading(2f));
                return;
            }
            
            int needBullets = 6 - m_bulletCount;
            int availableBullets = m_bulletItemCount - needBullets;
            
            if (availableBullets >= 0)
            {
                m_bulletItemCount -= needBullets;
                m_bulletCount = 6;
            }
            else
            {
                m_bulletCount = m_bulletCount + m_bulletItemCount;
                m_bulletItemCount =0;
            }
            
            StartCoroutine(WaitReloading(2f));
        }

        IEnumerator WaitReloading(float time)
        {
            yield return new WaitForSeconds(time);
            Debug.Log("Reloading is over");
            m_isReloading = false;
        }
    }
}