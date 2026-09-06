using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Header("Door GameObjects")]
    [Space]
    [SerializeField] private GameObject _leftDoor;
    [SerializeField] private GameObject _rightDoor;

    [Header("Left Rotations")]
    [Space]
    [SerializeField] private Vector3 _leftRotationOpen;
    [SerializeField] private Vector3 _leftRotationClosed;

    [Header("Right Rotations")]
    [Space]
    [SerializeField] private Vector3 _rightRotationOpen;
    [SerializeField] private Vector3 _rightRotationClosed;

    [Header("is open")]
    [Space]
    [SerializeField] private bool _isOpen;


}
