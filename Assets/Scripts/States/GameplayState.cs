using Players;
using UnityEngine;

public class GameplayState : StateBase
{
    [SerializeField] private Player m_player;

    private GameStateMachine m_gameStateMachine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_gameStateMachine.Enter<PauseState>();
        }
        if (m_player.health <= 0)
        {
            m_gameStateMachine.Enter<DeadState>();
        }
    }

    public override void Initialize(GameStateMachine gameStateMachine)
    {
        enabled = false;
        m_gameStateMachine = gameStateMachine;
    }

    public override void Enter()
    {
        enabled = true;
    }

    public override void Exit()
    {
        enabled = false;
    }

}
