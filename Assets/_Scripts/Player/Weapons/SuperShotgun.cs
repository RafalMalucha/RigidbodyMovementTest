using UnityEngine;

public class SuperShotgun : MonoBehaviour, IWeapon
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private LayerMask _interactableLayer;
    [Space]
    [Space]

    [Header("Super Shotgun Setup")]
    [Space]
    [SerializeField] private int _amountOfPellets;
    [SerializeField] private int _singlePelletDamage;
    [SerializeField][Range(0.0f, 1.0f)] private float _critChance;
    [SerializeField][Range(2.0f, 8.0f)] private float _critDamageModifier;
    [SerializeField] private float _pelletRange;
    [SerializeField] private float _maxSpreadAngle;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _playerPushbackForce;
    [SerializeField] private float _enemyPushbackForce;

    private Ray _pelletRay;
    private RaycastHit _pelletHit;

    private float _lastAttackTime = 0f;

    public void Attack()
    {
        Debug.Log("SSG Attack");

        if (Time.time < _lastAttackTime + _attackCooldown)
            return;

        _lastAttackTime = Time.time;

        for (int i = 0; i < _amountOfPellets; i++)
        {
            Quaternion randomPelletSpreadAngle = Quaternion.Euler(
                Random.Range(-_maxSpreadAngle, _maxSpreadAngle),
                Random.Range(-_maxSpreadAngle, _maxSpreadAngle),
                0f);

            Debug.DrawRay(
                _playerCamera.transform.position,
                randomPelletSpreadAngle * _playerCamera.transform.forward * _pelletRange,
                Color.orangeRed,
                5f
            );

            _pelletRay = new Ray(_playerCamera.transform.position, randomPelletSpreadAngle * _playerCamera.transform.forward);

            if (Physics.Raycast(_pelletRay, out _pelletHit, _pelletRange, _interactableLayer))
            {
                if (_pelletHit.collider.GetComponentInParent<Enemy_HealthController>() is Enemy_HealthController enemy)
                {
                    enemy.EnemyOnRaycastHit(_singlePelletDamage, _pelletRay.direction, _enemyPushbackForce);
                }
            }

            _playerRigidbody.AddForce(_playerCamera.transform.forward * -_playerPushbackForce, ForceMode.Impulse);
        }

    }
}
