using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_WallrunController : MonoBehaviour
{
    [Header("Player_WallrunController Setup")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _maxWallrunDuration;

    private Player_State _currentState;
    private Vector3 _wallNormal;
    private float _wallrunStartTime;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_JumpMessage>(OnPlayerJumpMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_WallrunEnterMessage>(OnPlayerWallrunEnterMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_WallrunExitMessage>(OnPlayerWallrunExitMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_JumpMessage>(OnPlayerJumpMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_WallrunEnterMessage>(OnPlayerWallrunEnterMessageReceived);
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_WallrunExitMessage>(OnPlayerWallrunExitMessageReceived);
    }

    void OnPlayerStateMessageReceived(Player_StateMessage message)
    {
        _currentState = message.Player_State;
    }

    void OnPlayerJumpMessageReceived(Player_JumpMessage message)
    {
        Debug.Log("wallrun controller jump");
        if (_currentState == Player_State.WallRunning)
        {
            StartCoroutine(WallrunCancel());
        }
    }

    void OnPlayerWallrunEnterMessageReceived(Player_WallrunEnterMessage message)
    {
        _wallNormal = message.WallNormal;
        Wallrun();
    }

    void OnPlayerWallrunExitMessageReceived(Player_WallrunExitMessage message)
    {
        Debug.Log("wallrun end");
    }

    private void Wallrun()
    {
        _wallrunStartTime = Time.time;

        Vector3 wallrunDirection = Vector3.Cross(_wallNormal, Vector3.down);

        if (Vector3.Angle(transform.forward, wallrunDirection) > 170f)
        {
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_WallrunLimitRotationMessage(-wallrunDirection));
            _rigidbody.AddForce(transform.forward * 1500f, ForceMode.Force);
        }
        else
        {
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_WallrunLimitRotationMessage(wallrunDirection));
            _rigidbody.AddForce(transform.forward * 1500f, ForceMode.Force);
        }
    }

    IEnumerator WallrunCancel()
    {
        _rigidbody.AddForce(_wallNormal * 300f, ForceMode.Impulse);
        _rigidbody.AddForce(transform.rotation * Vector3.forward * 200f, ForceMode.Impulse);
        yield return new WaitForFixedUpdate();
        _rigidbody.AddForce(Vector3.up * 75f, ForceMode.Impulse);
        yield return new WaitForFixedUpdate();
    }
}
