using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Fodder_MoveController : MonoBehaviour, IEnemy_MoveController
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _viewRange;
    [SerializeField] private SphereCollider _perceptionSphere;
    [SerializeField] private GameObject _testTargetDestination;

    private Vector3 _currentDestination;
    private Enemy_State _currentEnemyState;
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _currentEnemyState = GetNextState();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _currentDestination = GetAttackSpot();
    }

    private void FixedUpdate()
    {
        _navMeshAgent.destination = _testTargetDestination.transform.position;
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
