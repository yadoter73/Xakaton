using UnityEngine;

public class UIParallax : MonoBehaviour
{
    [Header("Настройки")]
    public float parallaxMultiplier = 20f;
    public float smoothSpeed = 5f;

    private Vector2 startPosition;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        // Получаем позицию мыши относительно центра экрана
        Vector2 mousePos = Input.mousePosition;
        float xOffset = (mousePos.x / Screen.width) - 0.5f;
        float yOffset = (mousePos.y / Screen.height) - 0.5f;

        Vector2 targetPosition = startPosition + new Vector2(xOffset * parallaxMultiplier, yOffset * parallaxMultiplier);

        // Плавно перемещаем постер
        transform.localPosition = Vector2.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothSpeed);
    }
}