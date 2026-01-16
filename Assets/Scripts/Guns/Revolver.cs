using UnityEngine;

namespace Guns
{
    public class Revolver : MonoBehaviour
    {
        [SerializeField] private float m_damage = 21f;

        [SerializeField] private float m_fireRate = 10f;

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
        
        [SerializeField] private Animator m_animator;

        private void start()
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
            
            if (m_isFiring && Time.time > m_nextFire)
            {
                m_nextFire = Time.time + 1f / m_fireRate;
                Shoot();
            }
            m_isFiring = false;
            m_animator.Play("PrepareForShooting");
        }
        
        private void Shoot()
        {
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
    }
}