using UnityEngine;
using PrimeTween;
public class BottleMovement : MonoBehaviour
{
    [SerializeField] private float _duration = 0.5f;
    public void SetTarget(Transform target)
    {
        if (target == null) return;
        Tween.Position(transform, target.position, _duration, Ease.InQuad)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
        transform.LookAt(target);
    }
}