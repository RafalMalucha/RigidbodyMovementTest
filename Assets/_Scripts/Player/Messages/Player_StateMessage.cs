using UnityEngine;

public struct Player_StateMessage
{
    public Player_State Player_State;

    public Player_StateMessage(Player_State player_State)
    {
        Debug.Log("setting new state " + player_State);
        Player_State = player_State;
    }
}
