using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CameraMoveForward m_camera;
    [SerializeField] private GameObject m_fader;
    [SerializeField] private AudioSource m_source;
    [SerializeField] private AudioClip m_clip;

    private void Start()
    {
        m_source.clip = m_clip;
        m_source.Play();
    }

    public void OnPlay()
    {
        m_camera.MoveForward();
        m_fader.SetActive(true);
        StartCoroutine(LoadScene());
    }

    public void OnExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(GlobalConstants.Scenes.Main);
    }
}
