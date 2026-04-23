using UnityEngine;
using PrimeTween;

public class MenuScroller : MonoBehaviour
{
    public float speed = 50f;
    public Transform centerPoint;
    public float resetThresholdY = -1000f; // Точка, после которой всё сбрасывается
    public bool isPaused;

    Vector3 startPos;
    Tween moveTween;

    void Awake() => startPos = transform.position;

    void Update()
    {
        if (isPaused) return;

        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Если уехали ниже порога — прыгаем назад
        if (transform.position.y <= resetThresholdY)
        {
            transform.position = startPos;
        }
    }

    public void CenterOn(Transform target)
    {
        isPaused = true;
        moveTween.Stop();
        float offsetY = centerPoint.position.y - target.position.y;
        moveTween = Tween.PositionY(transform, transform.position.y + offsetY, 0.3f, Ease.OutQuad);
    }

    public void Resume() => isPaused = false;
}