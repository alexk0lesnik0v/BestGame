using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_playBtn;
    [SerializeField] private Button m_exitBtn;
    //public Button btn;
    //private void OnEnable()
    //{
    //    btn.onClick.AddListener(OnPlay);
    //}

    public void OnPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OnExit()
    {
        Application.Quit();
    }

}
