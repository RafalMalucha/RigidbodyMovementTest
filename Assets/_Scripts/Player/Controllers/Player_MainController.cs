using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Player_MainController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _playerGravity;

    private bool _isGrounded;
    private Vector3 _boxCastOriginOffset = new Vector3(0f, 0.01f, 0f);
    private Vector3 _boxCastHalfExtents = new Vector3(0.5f, 0.05f, 0.5f);

    void Awake()
    {
        foreach(PlayerControllerSettings setting in GameBootstrap.PlayerControllersSettings.controllersSettings)
        {
            Type controllerType = Type.GetType(setting.controllerName);
            Component component = GetComponent(controllerType);

            if (component is Behaviour behaviour)
            {
                behaviour.enabled = setting.isActive;
            }

        }
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _rigidbody.AddForce(Vector3.down * _playerGravity, ForceMode.Force);
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
