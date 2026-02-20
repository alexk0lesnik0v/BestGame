using UnityEngine;

public class GameplayState : StateBase
{

    private GameStateMachine m_gameStateMachine;

    public override void Initialize(GameStateMachine gameStateMachine)
    {
        m_gameStateMachine = gameStateMachine;
    }

    public override void Enter()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_gameStateMachine.Enter<PauseState>();
        }
    }

    public override void Exit() { }

}
