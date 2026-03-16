using Players;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseState : StateBase
{
    [SerializeField] private GameObject m_pauseView;
    [SerializeField] private Button m_mainMenuBtn;
    [SerializeField] private Button m_resumeBtn;
    [SerializeField] private PlayerController m_player;
    [SerializeField] private GameObject m_inventoryView;

    private GameStateMachine m_gameStateMachine;

    private void OnEnable()
    {
        m_mainMenuBtn.onClick.AddListener(OnMainMenu);
        m_resumeBtn.onClick.AddListener(OnResume);
    }

    private void OnDisable()
    {
        m_mainMenuBtn.onClick.RemoveListener(OnMainMenu);
        m_resumeBtn.onClick.RemoveListener(OnResume);
    }

    public override void Initialize(GameStateMachine gameStateMachine)
    {
        m_gameStateMachine = gameStateMachine;
    }

    public override void Enter()
    {
        Time.timeScale = 0f;
        m_player.m_isNotFiring = true;
        m_pauseView.SetActive(true);
        m_inventoryView.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void Exit()
    {
        m_pauseView.SetActive(false);
        m_inventoryView.SetActive(true);
    }

    private void OnMainMenu()
    {
        SceneManager.LoadScene(GlobalConstants.Scenes.Menu);
        Time.timeScale = 1f;
    }

    private void OnResume()
    {
        m_gameStateMachine.Enter<GameplayState>();
        Time.timeScale = 1f;
        m_pauseView.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_player.m_isNotFiring = false;
    }
}
