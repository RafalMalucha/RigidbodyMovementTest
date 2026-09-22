using UnityEngine;

public struct Player_LookMessage
{
    public Vector2 LookDelta;

    public Player_LookMessage(Vector2 lookDelta)
    {
        LookDelta = lookDelta;
    }
}
