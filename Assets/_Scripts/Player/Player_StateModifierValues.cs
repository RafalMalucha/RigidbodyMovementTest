using UnityEngine;

[CreateAssetMenu(fileName = "Player_StateModifierValues", menuName = "Custom/Player_StateModifierValues")]
public class Player_StateModifierValues : ScriptableObject
{
    [SerializeField] private float _playerGravity;
    [SerializeField] private float _moveForce;
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _dashForce;

    public float GetPlayerGravity()
    {
        return _playerGravity;
    }

    public float GetMoveForce()
    {
        return _moveForce;
    }

    public float GetMaxMoveSpeed()
    {
        return _maxMoveSpeed;
    }

    public float GetJumpForce()
    {
        return _jumpForce;
    }

    public float GetDashForce()
    {
        return _dashForce;
    }
}
