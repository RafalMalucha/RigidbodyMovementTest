using UnityEngine;

public class Player_StateMachine : MonoBehaviour
{
    private Player_State _currentState;

    private bool _currentIsGroundedHelper;

    private void Awake()
    {
        _currentState = Player_State.Grounded;
        SetNewState(_currentState);
        GameBootstrap.MessageBus.Subscribe<Player_IsGroundedMessage>(OnPlayerIsGroundedMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_DashStartMessage>(OnPlayerDashStartMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_DashFinishMessage>(OnPlayerDashFinishMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_SlideStartMessage>(OnPlayerSlideStartMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_SlideFinishMessage>(OnPlayerSlideFinishMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_WallrunEnterMessage>(OnPlayerWallrunEnterMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_WallrunExitMessage>(OnPlayerWallrunExitMessageReceived);
    }

    void OnPlayerIsGroundedMessageReceived(Player_IsGroundedMessage message)
    {
        Debug.Log("Received grounded message " + message.IsGrounded);
        _currentIsGroundedHelper = message.IsGrounded;
        if (_currentState == Player_State.Sliding)
            return;

        if (_currentState == Player_State.Dashing)
            return;

        if (_currentState != Player_State.Grounded || _currentState != Player_State.Airborne)
            DecideOnGroundedState();
    }

    void OnPlayerDashStartMessageReceived(Player_DashStartMessage message)
    {
        SetNewState(Player_State.Dashing);
    }

    void OnPlayerDashFinishMessageReceived(Player_DashFinishMessage message)
    {
        DecideOnGroundedState();
    }

    void OnPlayerSlideStartMessageReceived(Player_SlideStartMessage message)
    {
        SetNewState(Player_State.Sliding);
    }

    void OnPlayerSlideFinishMessageReceived(Player_SlideFinishMessage message)
    {
        DecideOnGroundedState();
    }

    void OnPlayerWallrunEnterMessageReceived(Player_WallrunEnterMessage message)
    {
        if (_currentState == Player_State.Airborne)
        {
            Debug.Log("state machine wallrun enter");
            SetNewState(Player_State.WallRunning);
        }
    }

    void OnPlayerWallrunExitMessageReceived(Player_WallrunExitMessage message)
    {
        DecideOnGroundedState();
    }

    private void DecideOnGroundedState()
    {
        if (_currentIsGroundedHelper)
        {
            SetNewState(Player_State.Grounded);
        }
        else
        {
            SetNewState(Player_State.Airborne);
        }
    }

    private void SetNewState(Player_State newState)
    {
        if (_currentState == newState)
            return;

        _currentState = newState;

        GameBootstrap.MessageBus.Publish(new Player_StateMessage(_currentState));
        Debug.LogWarning(_currentState);
    }
}
