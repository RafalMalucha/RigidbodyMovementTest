using UnityEngine;

public struct Player_StateModifierMessage
{
    public Player_Modifier Player_Modifier;

    public Player_StateModifierMessage(Player_Modifier playerModifier)
    {
        Player_Modifier = playerModifier;
    }
}
