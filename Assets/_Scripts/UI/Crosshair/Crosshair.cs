using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class Crosshair : MonoBehaviour
{
    [Header("Crosshair Customisation Parameters")]
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private Color _outlineColor = Color.black;
    [SerializeField] private float _vSize;
    [SerializeField] private float _hSize;
    [SerializeField] private float _vGap;
    [SerializeField] private float _hGap;
    [SerializeField] private float _thickness;
    [SerializeField] private float _outlineThickness;
    [SerializeField] private bool _dot;
    [SerializeField] private bool _tShape;
    [SerializeField] private bool _outline;

    [Header("Crosshair Elements")]
    [SerializeField] private GameObject _dotImage;
    [SerializeField] private GameObject _topImage;
    [SerializeField] private GameObject _rightImage;
    [SerializeField] private GameObject _bottomImage;
    [SerializeField] private GameObject _leftImage;

    [Header("Crosshair Elements Outlines")]
    [SerializeField] private GameObject _dotOutline;
    [SerializeField] private GameObject _topOutline;
    [SerializeField] private GameObject _rightOutline;
    [SerializeField] private GameObject _bottomOutline;
    [SerializeField] private GameObject _leftOutline;

    private void OnValidate()
    {
        DrawCrosshair();
    }

    private void DrawCrosshair()
    {
        SetOutline();
        ApplyColor();
        ApplyOutlineColor();
        SetDotSize();
        SetDotOutlineSize();
        SetVGap();
        SetHGap();
        SetSizeAndThickness();
        SetOutlineSizeAndThickness();
        SetDot();
        SetTShape();
    }

    private void ApplyColor()
    {
        SetColor(_dotImage.GetComponent<RawImage>(), _color);
        SetColor(_topImage.GetComponent<RawImage>(), _color);
        SetColor(_rightImage.GetComponent<RawImage>(), _color);
        SetColor(_bottomImage.GetComponent<RawImage>(), _color);
        SetColor(_leftImage.GetComponent<RawImage>(), _color);
    }

    private void ApplyOutlineColor()
    {
        SetColor(_dotOutline.GetComponent<RawImage>(), _outlineColor);
        SetColor(_topOutline.GetComponent<RawImage>(), _outlineColor);
        SetColor(_rightOutline.GetComponent<RawImage>(), _outlineColor);
        SetColor(_bottomOutline.GetComponent<RawImage>(), _outlineColor);
        SetColor(_leftOutline.GetComponent<RawImage>(), _outlineColor);
    }

    private void SetColor(RawImage image, Color color)
    {
        if (image != null)
            image.color = color;
    }

    private void SetDot()
    {
        _dotImage.SetActive(_dot);
        _dotOutline.SetActive(_dot && _outline);
    }

    private void SetDotSize()
    {
        _dotImage.GetComponent<RectTransform>().sizeDelta = new Vector2(_thickness, _thickness);
    }

    private void SetDotOutlineSize()
    {
        _dotOutline.GetComponent<RectTransform>().sizeDelta = new Vector2(_thickness + _outlineThickness, _thickness + _outlineThickness);
    }

    private void SetTShape()
    {
        _topImage.SetActive(!_tShape);
        _topOutline.SetActive(!_tShape && _outline);
    }

    private void SetOutline()
    {
        _dotOutline.SetActive(_outline);
        _topOutline.SetActive(_outline);
        _rightOutline.SetActive(_outline);
        _bottomOutline.SetActive(_outline);
        _leftOutline.SetActive(_outline);
    }

    private void SetVGap()
    {
        _topImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, _vGap);
        _bottomImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -_vGap);

        _topOutline.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, _vGap);
        _bottomOutline.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -_vGap);
    }

    private void SetHGap()
    {
        _rightImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(_hGap, 0f);
        _leftImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-_hGap, 0f);

        _rightOutline.GetComponent<RectTransform>().anchoredPosition = new Vector2(_hGap, 0f);
        _leftOutline.GetComponent<RectTransform>().anchoredPosition = new Vector2(-_hGap, 0f);
    }

    private void SetSizeAndThickness()
    {
        _topImage.GetComponent<RectTransform>().sizeDelta = new Vector2(_thickness, _vSize);
        _bottomImage.GetComponent<RectTransform>().sizeDelta = new Vector2(_thickness, _vSize);

        _rightImage.GetComponent<RectTransform>().sizeDelta = new Vector2(_hSize, _thickness);
        _leftImage.GetComponent<RectTransform>().sizeDelta = new Vector2(_hSize, _thickness);
    }

    private void SetOutlineSizeAndThickness()
    {
        _topOutline.GetComponent<RectTransform>().sizeDelta = new Vector2(_thickness + _outlineThickness, _vSize + _outlineThickness);
        _bottomOutline.GetComponent<RectTransform>().sizeDelta = new Vector2(_thickness + _outlineThickness, _vSize + _outlineThickness);

        _rightOutline.GetComponent<RectTransform>().sizeDelta = new Vector2(_hSize + _outlineThickness, _thickness + _outlineThickness);
        _leftOutline.GetComponent<RectTransform>().sizeDelta = new Vector2(_hSize + _outlineThickness, _thickness + _outlineThickness);
    }

    public void IncreaseVSize()
    {
        _vSize += 2f;
        DrawCrosshair();
    }

    public void DecreaseVSize()
    {
        _vSize -= 2f;
        DrawCrosshair();
    }

    public void IncreaseHSize()
    {
        _hSize += 2f;
        DrawCrosshair();
    }

    public void DecreaseHSize()
    {
        _hSize -= 2f;
        DrawCrosshair();
    }

    public void IncreaseVGap()
    {
        _vGap += 2f;
        DrawCrosshair();
    }

    public void DecreaseVGap()
    {
        _vGap -= 2f;
        DrawCrosshair();
    }

    public void IncreaseHGap()
    {
        _hGap += 2f;
        DrawCrosshair();
    }

    public void DecreaseHGap()
    {
        _hGap -= 2f;
        DrawCrosshair();
    }

    public void IncreaseThickness()
    {
        _thickness += 2f;
        DrawCrosshair();
    }

    public void DecreaseThickness()
    {
        _thickness -= 2f;
        DrawCrosshair();
    }

    public void IncreaseOutlineThickness()
    {
        _outlineThickness += 2f;
        DrawCrosshair();
    }

    public void DecreaseOutlineThickness()
    {
        _outlineThickness -= 2f;
        DrawCrosshair();
    }

    public void DotToggle()
    {
        _dot = !_dot;
        DrawCrosshair();
    }

    public void TShapeToggle()
    {
        _tShape = !_tShape;
        DrawCrosshair();
    }

    public void OutlineToggle()
    {
        _outline = !_outline;
        DrawCrosshair();
    }

    public void SetCrosshairColor(Color color)
    {
        _color = color;
        DrawCrosshair();
    }

    public void SetOutlineColor(Color color)
    {
        _outlineColor = color;
        DrawCrosshair();
    }
}
