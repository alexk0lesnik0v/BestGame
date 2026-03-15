using Players;
using UnityEngine;

namespace Enemies
{
    public class TurnAround : MonoBehaviour
    {
        [SerializeField] private GameObject m_text;
        [SerializeField] private Gost m_gost;
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_audioClip;
        
        private bool m_isPlaying = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var player))
            {
                m_text.SetActive(true);
                m_gost.gameObject.SetActive(true);

                if (!m_isPlaying)
                {
                    m_audioSource.PlayOneShot(m_audioClip);
                    m_isPlaying = true;
                }
                
                Destroy(this.gameObject, 2f);
            }
        }
    }
}