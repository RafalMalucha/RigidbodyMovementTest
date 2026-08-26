using UnityEngine;

public class Player_MeleeController : MonoBehaviour
{
    [Header("Player_MeleeController Setup")]
    [SerializeField] private bool _test;

    void OnEnable()
    {
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_MeleeMessage>(OnPlayerMeleeMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_MeleeMessage>(OnPlayerMeleeMessageReceived);
    }

    void OnPlayerMeleeMessageReceived(Player_MeleeMessage message)
    {
        Melee();
    }

    void Melee()
    {
        Debug.Log("player melee behavior");
    }
}
