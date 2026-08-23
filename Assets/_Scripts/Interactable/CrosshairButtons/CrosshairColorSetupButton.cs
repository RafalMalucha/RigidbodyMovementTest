using UnityEngine;
using UnityEngine.Events;

public class CrosshairColorSetupButton : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent<Color32> _onInteract;
    [SerializeField] private Color32 _color;

    public void Interact()
    {
        _onInteract?.Invoke(_color);
    }
}
