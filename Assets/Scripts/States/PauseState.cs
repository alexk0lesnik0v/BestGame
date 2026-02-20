using UnityEngine;

public class PauseState : StateBase
{
    [SerializeField] private GameObject m_pauseView;
    private GameStateMachine m_gameStateMachine;
    public override void Initialize(GameStateMachine gameStateMachine)
    {
        m_gameStateMachine = gameStateMachine;
    }
    public override void Enter()
    {
        m_pauseView.SetActive(true);
    }

    public override void Exit()
    {
        m_pauseView.SetActive(false);
    }
}
