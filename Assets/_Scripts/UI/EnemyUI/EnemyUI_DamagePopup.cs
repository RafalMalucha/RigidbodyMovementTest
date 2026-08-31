using System.Collections;
using UnityEngine;
using TMPro;

public class EnemyUI_DamagePopup : MonoBehaviour
{
    [SerializeField] private GameObject _damagePopupPrefab;
    [SerializeField] private float _damagePopupLifetime;

    public void PlayDamagePopup(int damageAmount)
    {
        StartCoroutine(DamagePopupCoroutine());
    }

    private IEnumerator DamagePopupCoroutine()
    {
        GameObject popup = Instantiate(_damagePopupPrefab);

        popup.transform.SetParent(transform);
        popup.transform.SetAsFirstSibling();

        popup.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, 1f, 0f);
        popup.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        yield return new WaitForSeconds(_damagePopupLifetime);
        Destroy(popup);
    }
}
