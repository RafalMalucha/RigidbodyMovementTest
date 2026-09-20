using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Fodder_MoveController : MonoBehaviour, IEnemy_MoveController
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _viewRange;

    private Transform _currentDestination;
    private Enemy_State _currentEnemyState;

    private void Start()
    {
        _currentEnemyState = GetNextState();
    }

    private void FixedUpdate()
    {

    }

    public Enemy_State GetNextState()
    {
        return Enemy_State.Idle;
    }

    public IEnumerator Move()
    {
        yield return null;
    }

    public Vector3 GetSafeSpot()
    {
        return new Vector3(0f, 0f, 0f);
    }

    public Vector3 GetAttackSpot()
    {
        return new Vector3(0f, 0f, 0f);
    }

    public bool CanSeePlayer()
    {
        return false;
    }

    public Vector3 GetPlayerPosition()
    {
        return new Vector3(0f, 0f, 0f);
    }
}
