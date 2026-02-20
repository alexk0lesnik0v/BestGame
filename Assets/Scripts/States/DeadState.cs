using UnityEngine;

public class DeadState : StateBase
{
    [SerializeField] private GameObject m_deathView;
    private GameStateMachine m_gameStateMachine;
    public override void Initialize(GameStateMachine gameStateMachine)
    {
        m_gameStateMachine = gameStateMachine;
    }
    public override void Enter()
    {
        m_deathView.SetActive(true);
    }

    public override void Exit()
    {
        m_deathView.SetActive(false);
    }
}
