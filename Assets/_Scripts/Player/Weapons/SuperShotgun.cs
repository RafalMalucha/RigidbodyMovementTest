using UnityEngine;

public class SuperShotgun : MonoBehaviour, IWeapon
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private LayerMask _interactableLayer;

    [Header("Super Shotgun Setup")]
    [SerializeField] private int _amountOfPellets;
    [SerializeField] private int _singlePelletDamage;
    [SerializeField] private float _pelletRange;
    [SerializeField] private float _maxSpreadAngle;

    private Ray _pelletRay;
    private RaycastHit _pelletHit;

    public void Attack()
    {
        Debug.Log("SSG Attack");

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
                    enemy.EnemyOnRaycastHit(_singlePelletDamage, _pelletRay.direction);
                }
            }

            _playerRigidbody.AddForce(_playerCamera.transform.forward * -3f, ForceMode.Impulse);
        }

    }
}
