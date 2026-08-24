using UnityEngine;

public class MonkeyBar : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_MonkeyBarEnterMessage());
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_MonkeyBarExitMessage());
        }
    }
}
