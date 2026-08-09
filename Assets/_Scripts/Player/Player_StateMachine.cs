using UnityEngine;

public class Player_StateMachine : MonoBehaviour
{
    public Player_State CurrentState { get; private set; }

    private void Awake()
    {
        CurrentState = Player_State.Grounded;
    }
}
