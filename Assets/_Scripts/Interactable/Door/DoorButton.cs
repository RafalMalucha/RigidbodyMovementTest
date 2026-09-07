using UnityEngine;
using UnityEngine.Events;

public class DoorButton : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent _openDoor;
    [SerializeField] private UnityEvent _closeDoor;

    private bool _doorOpen = true;

    public void Interact()
    {
        if (_doorOpen)
        {
            Debug.Log("door close button press");
            _doorOpen = false;
            _closeDoor?.Invoke();
        }
        else
        {
            Debug.Log("door open button press");
            _doorOpen = true;
            _openDoor?.Invoke();
        }
    }
}
