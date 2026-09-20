using System.Collections;
using UnityEngine;

public interface IEnemy_MoveController
{
    IEnumerator Move();

    Enemy_State GetNextState();

    Vector3 GetSafeSpot();
    Vector3 GetAttackSpot();

    bool CanSeePlayer();
    Vector3 GetPlayerPosition();
}
