using UnityEngine;

public struct Player_MoveMessage
{
    public Vector2 MoveInput;

    public Player_MoveMessage(Vector2 moveInput)
    {
        MoveInput = moveInput;
    }
}
