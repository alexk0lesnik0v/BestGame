using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class WinMenu : MonoBehaviour
    {
        [SerializeField] private Button m_mainmenuButton;
        
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}