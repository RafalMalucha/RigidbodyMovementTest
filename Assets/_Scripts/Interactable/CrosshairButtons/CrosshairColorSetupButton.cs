using UnityEngine;
using UnityEngine.Events;

public class CrosshairColorSetupButton : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent<Color> _onInteract;
    [SerializeField] private Color _color;

    public void Interact()
    {
        _onInteract?.Invoke(_color);
    }
}
