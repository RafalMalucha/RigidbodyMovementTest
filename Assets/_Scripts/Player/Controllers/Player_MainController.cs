using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Player_MainController : MonoBehaviour
{
    //[SerializeField] private Player_StateModifierValues _stateModifierValues;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _playerGravity;

    private Player_State _currentState;
    private bool _isGrounded;
    private bool _onSlope;
    private Vector3 _boxCastOriginOffset = new Vector3(0f, 0.01f, 0f);
    private Vector3 _boxCastHalfExtents = new Vector3(0.5f, 0.05f, 0.5f);

    void Awake()
    {
        foreach (PlayerControllerSettings setting in GameBootstrap.PlayerControllersSettings.controllersSettings)
        {
            Type controllerType = Type.GetType(setting.controllerName);
            Component component = GetComponent(controllerType);

            if (component is Behaviour behaviour)
            {
                behaviour.enabled = setting.isActive;
            }

        }
        _rigidbody = GetComponent<Rigidbody>();
        GameBootstrap.MessageBus.Subscribe<Player_OnSlopeMessage>(OnPlayerOnSlopeMessageReceived);
        GameBootstrap.MessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
    }

    void OnPlayerOnSlopeMessageReceived(Player_OnSlopeMessage message)
    {
        _onSlope = message.OnSlope;
    }

    void OnPlayerStateMessageReceived(Player_StateMessage message)
    {
        _currentState = message.Player_State;
    }

    void FixedUpdate()
    {
        if(_currentState != Player_State.WallRunning)
        {
            _rigidbody.AddForce(_playerGravity * Vector3.down, ForceMode.Force);
        }

        GroundCheck();
    }


    private void GroundCheck()
    {
        bool grounded = CheckBoxIsGroundedCheck();

        if (grounded != _isGrounded)
        {
            _isGrounded = grounded;
            GameBootstrap.MessageBus.Publish(new Player_IsGroundedMessage(_isGrounded));
        }
    }

    private bool CheckBoxIsGroundedCheck()
    {
        ExtDebug.DrawBoxCastBox(
            transform.position + _boxCastOriginOffset,
            _boxCastHalfExtents,
            transform.rotation,
            Vector3.down,
            0.0f,
            Color.green
        );

        return Physics.CheckBox(
            transform.position + _boxCastOriginOffset,
            _boxCastHalfExtents,
            transform.rotation,
            LayerMask.GetMask("Level")
        );
    }

}
