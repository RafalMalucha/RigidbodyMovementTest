using UnityEngine;

public class Player_HealthController : MonoBehaviour
{
    [Header("Player_HealthController Setup")]
    [Space]
    [SerializeField] private int _maxPlayerHealth;

    private int _currentPlayerHealth;

    void OnEnable()
    {
        SetNewPlayerHealth(_maxPlayerHealth);
    }

    void Update()
    {
        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_HealthCurrentHealthMessage(_currentPlayerHealth));
    }

    private void SetNewPlayerHealth(int health)
    {
        _currentPlayerHealth = health;
        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_HealthCurrentHealthMessage(_currentPlayerHealth));
    }
}
