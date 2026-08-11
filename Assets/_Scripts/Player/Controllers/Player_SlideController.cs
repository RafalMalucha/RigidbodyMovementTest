using UnityEngine;

public class Player_SlideController : MonoBehaviour
{
    void OnEnable()
    {
        GameBootstrap.MessageBus.Subscribe<Player_SlideMessage>(OnPlayerSlideMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.MessageBus.Unsubscribe<Player_SlideMessage>(OnPlayerSlideMessageReceived);
    }

    void OnPlayerSlideMessageReceived(Player_SlideMessage message)
    {
        Slide();
    }

    void Slide()
    {
        Debug.Log("player slide behavior");
    }
}
