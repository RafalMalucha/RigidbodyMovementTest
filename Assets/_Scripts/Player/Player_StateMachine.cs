using UnityEngine;

public class Player_StateMachine : MonoBehaviour
{
    private Player_State _currentState;

    private void Awake()
    {
        _currentState = Player_State.Grounded;
        SetNewState(_currentState);
        GameBootstrap.MessageBus.Subscribe<Player_IsGroundedMessage>(OnPlayerIsGroundedMessageReceived);
    }

    void OnPlayerIsGroundedMessageReceived(Player_IsGroundedMessage message)
    {
        Debug.Log("Received grounded message " + message.IsGrounded);
        if(message.IsGrounded)
        {
            SetNewState(Player_State.Grounded);
        } else
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
    }
}
