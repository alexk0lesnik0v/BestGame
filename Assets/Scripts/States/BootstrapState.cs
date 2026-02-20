using UnityEngine;

public class BootstrapState : StateBase
{

    private GameStateMachine m_gameStateMachine;

    public override void Initialize(GameStateMachine gameStateMachine)
    {
        m_gameStateMachine = gameStateMachine;
    }

    public override void Enter()
    {
        m_gameStateMachine.Enter<GameplayState>();
    }

    public override void Exit() { }

}
