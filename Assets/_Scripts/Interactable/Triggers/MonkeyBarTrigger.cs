using UnityEngine;

public class MonkeyBar : MonoBehaviour
{
    [SerializeField] private GameObject _barObject;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameBootstrap.PlayerControllerMessageBus.Publish(new Player_MonkeyBarEnterMessage(_barObject.transform.position));
        }
    }
}
