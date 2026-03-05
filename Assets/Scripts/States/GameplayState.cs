using Players;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayState : StateBase
{
    [SerializeField] private Player m_player;

    private GameStateMachine m_gameStateMachine;
    private InputSystem_Actions m_gameInput;

    private void Awake()
    {
        m_gameInput = new InputSystem_Actions();
        m_gameInput.Enable();
    }

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

    //private void OnEnable()
    //{
    //    m_gameInput.Player.Escape.performed += OnEscape;
    //}

    //private void OnDisable()
    //{
    //    m_gameInput.Player.Escape.performed -= OnEscape;
    //}

    //private void OnEscape(InputAction.CallbackContext context)
    //{
    //    m_gameStateMachine.Enter<PauseState>();
    //}

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
