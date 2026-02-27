using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadState : StateBase
{
    [SerializeField] private GameObject m_deathView;
    [SerializeField] private Button m_restartBtn;
    [SerializeField] private Button m_mainMenuBtn;

    private GameStateMachine m_gameStateMachine;

    private void OnEnable()
    {
        m_restartBtn.onClick.AddListener(OnRestart);
        m_mainMenuBtn.onClick.AddListener(OnMainMenu);
    }

    private void OnDisable()
    {
        m_restartBtn.onClick.RemoveListener(OnRestart);
        m_mainMenuBtn.onClick.RemoveListener(OnMainMenu);
    }

    public override void Initialize(GameStateMachine gameStateMachine)
    {
        m_gameStateMachine = gameStateMachine;
    }
    public override void Enter()
    {
        m_deathView.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void Exit()
    {
        m_deathView.SetActive(false);
    }

    private void OnRestart()
    {
        SceneManager.LoadScene(GlobalConstants.Scenes.Main);
    }

    private void OnMainMenu()
    {
        SceneManager.LoadScene(GlobalConstants.Scenes.Menu);
    }
}
