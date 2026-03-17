using System;
using Players;
using UnityEngine;

namespace Enemies
{
    public class TurnAround : MonoBehaviour
    {
        [SerializeField] private GameObject m_text;
        [SerializeField] private GameObject m_gost;
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_audioClip;
        [SerializeField] private Interactor m_interactor;
        
        private bool m_isPlaying = false;

        private void Start()
        {
            m_interactor.OnGostView += OnGostActivate;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var player))
            {
                if (m_text is not null)
                {
                    m_text.SetActive(true);
                }
                
                m_gost.SetActive(true);

                if (!m_isPlaying)
                {
                    m_audioSource.PlayOneShot(m_audioClip);
                    m_isPlaying = true;
                }
                
                Destroy(this.gameObject, 2f);
            }
        }
        
        private void OnGostActivate()
        {
            Destroy(m_text.gameObject);
            m_interactor.OnGostView -= OnGostActivate;
        }
    }
}