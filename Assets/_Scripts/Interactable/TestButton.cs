using UnityEngine;
using UnityEngine.Events;

public class TestButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Player_Modifier _modifier;

    public void Interact()
    {
        Debug.LogWarning("button pressed");
        GameBootstrap.PlayerControllerMessageBus.Publish(new Player_RequestStateModifierChangeMessage_Debug(_modifier));
    }
}
