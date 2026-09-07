using System.Collections;
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


    private bool _isOpen;

    private Quaternion _leftOpenRotationQuaternion;
    private Quaternion _leftClosedRotationQuaternion;
    private Quaternion _rightOpenRotationQuaternion;
    private Quaternion _rightClosedRotationQuaternion;

    private void Awake()
    {
        _leftOpenRotationQuaternion = Quaternion.Euler(_leftRotationOpen);
        _leftClosedRotationQuaternion = Quaternion.Euler(_leftRotationClosed);
        _rightOpenRotationQuaternion = Quaternion.Euler(_rightRotationOpen);
        _rightClosedRotationQuaternion = Quaternion.Euler(_rightRotationClosed);
    }

    public void DoorOpen()
    {
        _isOpen = true;
        Debug.Log("Door open script run");
        StartCoroutine(LerpDoorRotation());
    }

    public void DoorClose()
    {
        _isOpen = false;
        Debug.Log("Door close script run");
        StartCoroutine(LerpDoorRotation());
    }

    IEnumerator LerpDoorRotation()
    {
        float duration = 2.0f;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;

            if (_isOpen)
            {
                _leftDoor.transform.rotation = Quaternion.Slerp(_leftClosedRotationQuaternion, _leftOpenRotationQuaternion, t);
                _rightDoor.transform.rotation = Quaternion.Slerp(_rightClosedRotationQuaternion, _rightOpenRotationQuaternion, t);
            }
            else
            {
                _leftDoor.transform.rotation = Quaternion.Slerp(_leftOpenRotationQuaternion, _leftClosedRotationQuaternion, t);
                _rightDoor.transform.rotation = Quaternion.Slerp(_rightOpenRotationQuaternion, _rightClosedRotationQuaternion, t);
            }

            yield return null;
        }

        if (_isOpen)
        {
            _leftDoor.transform.rotation = _leftOpenRotationQuaternion;
            _rightDoor.transform.rotation = _rightOpenRotationQuaternion;
        }
        else
        {
            _leftDoor.transform.rotation = _leftClosedRotationQuaternion;
            _rightDoor.transform.rotation = _rightClosedRotationQuaternion;
        }

    }
}
