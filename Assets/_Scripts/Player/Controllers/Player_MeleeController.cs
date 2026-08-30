using UnityEngine;
using UnityEngine.Events;

public class Player_MeleeController : MonoBehaviour
{
    [Header("Player_MeleeController Setup")]
    [Space]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _hittableLayer;

    [Header("Melee Setup")]
    [Space]
    [SerializeField] private Vector3 _meleeBoxcastHalfExtents;
    [SerializeField] private float _meleeRange;
    [SerializeField] private int _meleeHitDamage;
    [SerializeField] private int _meleeHitForce;

    private RaycastHit[] _boxCastHits;

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
        Debug.Log("melee");

        Vector3 origin = transform.position;
        Vector3 direction = _playerCamera.transform.forward;

        _boxCastHits = Physics.BoxCastAll(
            _playerCamera.transform.position + _playerCamera.transform.forward * 1.5f,
            _meleeBoxcastHalfExtents,
            _playerCamera.transform.forward,
            transform.rotation,
            _meleeRange,
            LayerMask.GetMask("Hittable")
        );

        foreach (RaycastHit hit in _boxCastHits)
        {
            if (hit.collider.GetComponentInParent<Enemy_HealthController>() is Enemy_HealthController enemy)
            {
                Debug.Log(hit.collider.name);
                enemy.EnemyOnMeleeHit(_meleeHitDamage, _meleeHitForce, _playerCamera.transform.forward);
            }
        }
    }

    void Update()
    {
        ExtDebug.DrawBoxCastBox(
            _playerCamera.transform.position + _playerCamera.transform.forward * 1.5f,
            _meleeBoxcastHalfExtents, transform.rotation,
            _playerCamera.transform.forward,
            _meleeRange,
            Color.coral
        );
    }
}
