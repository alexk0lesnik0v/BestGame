using UnityEngine;

namespace Enemies
{
    public class DollParts : MonoBehaviour
    {
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_deathSound;

        public void Start()
        {
            m_audioSource.PlayOneShot(m_deathSound);
        }
    }
}