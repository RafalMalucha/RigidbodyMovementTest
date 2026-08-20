using UnityEngine;

public struct Player_RequestStateModifierChangeMessage_Debug
{
    public Player_Modifier Modifier;

    public Player_RequestStateModifierChangeMessage_Debug(Player_Modifier modifier)
    {
        Modifier = modifier;
    }
}
