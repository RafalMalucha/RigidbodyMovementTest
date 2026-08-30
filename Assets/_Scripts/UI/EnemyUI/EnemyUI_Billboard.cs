using UnityEngine;

public class EnemyUI_Billboard : MonoBehaviour
{
    private GameObject _playerCamera;

    private void OnEnable()
    {
        _playerCamera = GameObject.Find("Main Camera");
    }

    private void LateUpdate()
    {
        transform.LookAt(_playerCamera.transform.position);
        transform.Rotate(0f, 180f, 0f);
    }
}
