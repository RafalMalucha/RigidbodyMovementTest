using UnityEngine;

public struct Player_IsGroundedMessage
{
    public bool IsGrounded;

    public Player_IsGroundedMessage(bool isGrounded)
    {
        Debug.Log(isGrounded);
        IsGrounded = isGrounded;
    }
}
