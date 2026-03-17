using Players;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class WinMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_winMenu;
        [SerializeField] private Button m_mainmenuButton;
        [SerializeField] private PlayerController m_player;
        [SerializeField] private GameObject m_inventoryView;
        [SerializeField] private GameObject m_timer;
        
        private void OnEnable()
        {
            m_mainmenuButton.onClick.AddListener(OnMainMenu);
        }
        private void OnDisable()
        {
            m_mainmenuButton.onClick.RemoveListener(OnMainMenu);
        }
        
        private void OnMainMenu()
        {
            SceneManager.LoadScene(GlobalConstants.Scenes.Menu);
            Time.timeScale = 1f;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var player))
            {
                Time.timeScale = 0f;
                m_player.m_isNotFiring = true;
                m_winMenu.SetActive(true);
                m_inventoryView.SetActive(false);
                m_timer.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}