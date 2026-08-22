using UnityEngine;
using UnityEngine.Events;

public class CrosshairSetupButton : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent _onInteract;

    public void Interact()
    {
        _onInteract?.Invoke();
    }
}
