using Players;
using UnityEngine;

namespace DefaultNamespace
{
    public class WinController :  MonoBehaviour
    {
        [SerializeField] private GameObject m_winUI;
        [SerializeField] private PlayerController m_player;
        [SerializeField] private AudioClip m_soundWin;
        [SerializeField] private AudioSource m_audioSource;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var player))
            {
                m_winUI.SetActive(true);
                m_audioSource.PlayOneShot(m_soundWin);
                m_player.enabled = false;
                m_player.m_isDead = true;
                
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}