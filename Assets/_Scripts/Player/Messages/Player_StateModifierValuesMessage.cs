using UnityEngine;

public struct Player_StateModifierValuesMessage
{
    public Player_StateModifierValues Player_StateModifierValues;

    public Player_StateModifierValuesMessage(Player_StateModifierValues player_StateModifierValues)
    {
        Debug.Log("setting new state modifier values " + player_StateModifierValues);
        Player_StateModifierValues = player_StateModifierValues;
    }
}
