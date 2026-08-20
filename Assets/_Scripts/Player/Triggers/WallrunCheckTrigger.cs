using UnityEngine;

public class WallrunCheckTriger : MonoBehaviour
{
    private Player_State _playerState;

    private void OnEnable()
    {
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
    }

    private void OnDisable()
    {
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_StateMessage>(OnPlayerStateMessageReceived);
    }

    void OnPlayerStateMessageReceived(Player_StateMessage message)
    {
        _playerState = message.Player_State;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Wallrun"))
        {
            Debug.LogWarning("wallrun start test");

            TryForWallrun();
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Wallrun") && _playerState == Player_State.Airborne)
        {
            TryForWallrun();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Wallrun"))
        {
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_WallrunExitMessage());
        }
    }

    private void TryForWallrun()
    {
        Debug.DrawRay(transform.parent.position + new Vector3(0f, 1.5f, 0f), transform.parent.rotation * Vector3.right * 1.1f, Color.red, 2f);
        Physics.Raycast(transform.parent.position + new Vector3(0f, 1.5f, 0f), transform.parent.rotation * Vector3.right, out RaycastHit hitRight, 1.1f, LayerMask.GetMask("Level"));
        Debug.DrawRay(transform.parent.position + new Vector3(0f, 1.5f, 0f), transform.parent.rotation * Vector3.left * 1.1f, Color.red, 2f);
        Physics.Raycast(transform.parent.position + new Vector3(0f, 1.5f, 0f), transform.parent.rotation * Vector3.left, out RaycastHit hitLeft, 1.1f, LayerMask.GetMask("Level"));

        if (hitLeft.normal == new Vector3(0f, 0f, 0f))
        {
            Debug.Log(hitRight.normal);
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_WallrunEnterMessage(hitRight.normal));
        }


        if (hitRight.normal == new Vector3(0f, 0f, 0f))
        {
            Debug.Log(hitLeft.normal);
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_WallrunEnterMessage(hitLeft.normal));
        }
    }
}
