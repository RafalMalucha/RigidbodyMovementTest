using System.Collections;
using UnityEngine;
using TMPro;

public class EnemyUI_DamagePopup : MonoBehaviour
{
    [SerializeField] private GameObject _damagePopupPrefab;
    [SerializeField] private float _damagePopupLifetime;

    [SerializeField] private float _minSize;
    [SerializeField] private float _maxSize;

    public void PlayDamagePopup(int damageAmount)
    {
        StartCoroutine(DamagePopupCoroutine(damageAmount));
    }

    private IEnumerator DamagePopupCoroutine(int damageAmount)
    {
        GameObject popup = Instantiate(_damagePopupPrefab);

        popup.transform.SetParent(transform);
        popup.transform.SetAsFirstSibling();

        var popupRectTransform = popup.GetComponent<RectTransform>();

        popupRectTransform.anchoredPosition3D = new Vector3(0f, 1.5f, -1f);
        popup.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        float damageNormalized = Mathf.Clamp01(damageAmount / 100f);
        float scale = Mathf.Lerp(_minSize, _maxSize, damageNormalized);
        popup.transform.localScale = new Vector3(scale, scale, 1f);

        popup.GetComponent<TextMeshProUGUI>().text = $"{damageAmount}";

        float elapsed = 0f;

        float startY = 1.5f;
        float velocityY = Random.Range(1f, 4f);
        float popupGravity = Random.Range(10f, 20f);

        float sideDistance = Random.Range(0.1f, 1.9f);
        float sideDirection = Random.value < 0.5f ? -1f : 1f;

        while (elapsed < _damagePopupLifetime)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / _damagePopupLifetime);
            float y = startY + velocityY * t - 0.5f * popupGravity * t * t;
            float x = sideDirection * sideDistance * t;

            popupRectTransform.anchoredPosition3D = new Vector3(x, y, -1f);

            yield return null;
        }

        Destroy(popup);
    }
}
