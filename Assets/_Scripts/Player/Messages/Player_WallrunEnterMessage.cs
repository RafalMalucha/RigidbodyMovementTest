using UnityEngine;

public struct Player_WallrunEnterMessage
{
    public Vector3 WallNormal;

    public Player_WallrunEnterMessage(Vector3 wallNormal)
    {
        WallNormal = wallNormal;
    }
}
