using UnityEngine;

public class Player_AttackController : MonoBehaviour
{
    void Awake()
    {
        GameBootstrap.MessageBus.Subscribe<Player_AttackMessage>(OnPlayerAttackMessageReceived);
    }

    void OnPlayerAttackMessageReceived(Player_AttackMessage message)
    {
        Attack();
    }

    void Attack()
    {
        Debug.Log("player attack behavior");
    }
}
