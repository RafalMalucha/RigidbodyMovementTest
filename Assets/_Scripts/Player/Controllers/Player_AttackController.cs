using UnityEngine;

public class Player_AttackController : MonoBehaviour
{
    [Header("Player_AttackController Setup")]
    [SerializeField] private GameObject _weaponHolder;

    void OnEnable()
    {
        GameBootstrap.PlayerControllerMessageBus.Subscribe<Player_AttackMessage>(OnPlayerAttackMessageReceived);
    }

    void OnDisable()
    {
        GameBootstrap.PlayerControllerMessageBus.Unsubscribe<Player_AttackMessage>(OnPlayerAttackMessageReceived);
    }

    void OnPlayerAttackMessageReceived(Player_AttackMessage message)
    {
        Attack();
    }

    void Attack()
    {
        Debug.Log("player attack behavior");
        _weaponHolder.GetComponentInChildren<IWeapon>().Attack();
    }
}
