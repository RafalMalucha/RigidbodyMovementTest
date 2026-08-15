using UnityEngine;

public struct Player_WallrunLimitRotationMessage
{
    public Vector3 WallrunDirection;

    public Player_WallrunLimitRotationMessage(Vector3 wallrunDirection)
    {
        WallrunDirection = wallrunDirection;
    }
}
