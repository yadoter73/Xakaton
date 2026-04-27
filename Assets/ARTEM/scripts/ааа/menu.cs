using UnityEngine;
using PrimeTween;

[RequireComponent(typeof(RectTransform))]
public class MenuScreen : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void MoveToY(float targetY, float duration)
    {
        Tween.UIAnchoredPositionY(rectTransform, targetY, duration, Ease.InOutCubic);
    }

    public void SetInstantPositionY(float targetY)
    {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();

        Vector2 pos = rectTransform.anchoredPosition;
        pos.y = targetY;
        rectTransform.anchoredPosition = pos;
    }
}