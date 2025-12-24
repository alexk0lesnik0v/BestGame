using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class DeathMenu : MonoBehaviour
    {
        [SerializeField] private Button m_restartButton;
        [SerializeField] private Button m_mainmenuButton;
        
        private void OnEnable()
        {
            m_restartButton.onClick.AddListener(OnRestart);
            m_mainmenuButton.onClick.AddListener(OnMainMenu);
        }
        private void OnDisable()
        {
            m_restartButton.onClick.RemoveListener(OnRestart);
            m_mainmenuButton.onClick.RemoveListener(OnMainMenu);
        }
        private void OnRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void OnMainMenu()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}