using UnityEngine;

public class Player_StateMachine : MonoBehaviour
{
    [Header("Player State Modifier Values - Scriptable Objects")]
    [SerializeField] private Player_StateModifierValues _stateModifierValues_Normal;
    [SerializeField] private Player_StateModifierValues _stateModifierValues_Cracked;
    [SerializeField] private Player_StateModifierValues _stateModifierValues_Slowed;
    [SerializeField] private Player_StateModifierValues _stateModifierValues_Stunned;

    private Player_Modifier _currentModifier;
    private Player_StateModifierValues _currentStateModifierValues;

    private Player_State _currentState;

    private bool _currentIsGroundedHelper;

    private void Awake()
    {
        _currentState = Player_State.Grounded;
        SetNewState(_currentState);

        _currentModifier = Player_Modifier.Normal;
        SetNewStateModifier(_currentModifier);

        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_IsGroundedMessage>(OnPlayerIsGroundedMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_DashStartMessage>(OnPlayerDashStartMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_DashFinishMessage>(OnPlayerDashFinishMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_SlideStartMessage>(OnPlayerSlideStartMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_SlideFinishMessage>(OnPlayerSlideFinishMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_WallrunEnterMessage>(OnPlayerWallrunEnterMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_WallrunExitMessage>(OnPlayerWallrunExitMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_RequestStateModifierChangeMessage_Debug>(OnPlayerRequestStateModifierChangeMessageReceived_Debug);
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
        if (_currentState == Player_State.Airborne || _currentState == Player_State.Dashing)
        {
            Debug.Log("state machine wallrun enter");
            SetNewState(Player_State.WallRunning);
        }
    }

    void OnPlayerWallrunExitMessageReceived(Player_WallrunExitMessage message)
    {
        DecideOnGroundedState();
    }

    void OnPlayerRequestStateModifierChangeMessageReceived_Debug(Player_RequestStateModifierChangeMessage_Debug message)
    {
        _currentModifier = message.Modifier;
        SetNewStateModifier(_currentModifier);
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

        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_StateMessage(_currentState));
        Debug.LogWarning(_currentState);
    }

    private void SetNewStateModifier(Player_Modifier newModifier)
    {
        _currentModifier = newModifier;
        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_StateModifierMessage(_currentModifier));

        switch (_currentModifier)
        {
            case Player_Modifier.Normal:
                GameBootstrap.PlayerControllerMessageBus.Publish(new Player_StateModifierValuesMessage(_stateModifierValues_Normal));
                break;
            case Player_Modifier.Cracked:
                GameBootstrap.PlayerControllerMessageBus.Publish(new Player_StateModifierValuesMessage(_stateModifierValues_Cracked));
                break;
            case Player_Modifier.Slowed:
                GameBootstrap.PlayerControllerMessageBus.Publish(new Player_StateModifierValuesMessage(_stateModifierValues_Slowed));
                break;
            case Player_Modifier.Stunned:
                GameBootstrap.PlayerControllerMessageBus.Publish(new Player_StateModifierValuesMessage(_stateModifierValues_Stunned));
                break;
            default:
                break;
        }
        Debug.LogWarning(_currentModifier);
    }
}
