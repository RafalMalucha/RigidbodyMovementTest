using UnityEngine;

public class Player_StateMachine : MonoBehaviour
{
    private Player_State _currentState;
    private bool _newStateFlag;

    private void Awake()
    {
        _currentState = Player_State.Grounded;
        _newStateFlag = true;
        GameBootstrap.MessageBus.Subscribe<Player_IsGroundedMessage>(OnPlayerIsGroundedMessageReceived);
    }

    void OnPlayerIsGroundedMessageReceived(Player_IsGroundedMessage message)
    {
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
        _currentState = newState;
        _newStateFlag = true;

    }

    private void Update()
    {
        if(_newStateFlag)
        {
            GameBootstrap.MessageBus.Publish(new Player_StateMessage(_currentState));
            _newStateFlag = false;
        }
    }
}
