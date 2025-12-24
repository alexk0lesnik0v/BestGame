using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_playBtn;
    [SerializeField] private Button m_exitBtn;

    private void OnEnable()
    {
        m_playBtn.onClick.AddListener(OnPlay);
        m_exitBtn.onClick.AddListener(OnExit);
    }
    private void OnDisable()
    {
        m_playBtn?.onClick.RemoveListener(OnPlay);
        m_exitBtn?.onClick.RemoveListener(OnExit);
    }
    public void OnPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OnExit()
    {
        Application.Quit();
    }

}
