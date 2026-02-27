using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_pauseMenu;
    [SerializeField] private Button m_resumeButton;
    [SerializeField] private Button m_pauseButton;
    
    private bool m_isPaused;

    void Update()
    {
    }
    public void OnPause()
    {
        //m_pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        m_isPaused = true;
    }
    public void OnResume()
    {
        m_pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        m_isPaused = false;
    }
    private void OnEnable()
    {
        m_resumeButton.onClick.AddListener(OnResume);
        m_pauseButton.onClick.AddListener(OnPause);
    }
    private void OnDisable()
    {
        m_resumeButton.onClick?.RemoveListener(OnResume);
        m_pauseButton.onClick?.RemoveListener(OnPause);
    }
}
