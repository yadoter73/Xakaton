using UnityEngine;
using PrimeTween;
public class BottleMovement : MonoBehaviour
{
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] GameObject _enemy;
    public void SetTarget(Transform target)
    {
        if (target == null) return;
        Tween.Position(transform,target.position, _duration, Ease.InQuad)
            .OnComplete(() =>
            {
                Destroy(gameObject);
                Tween.Delay(1).OnComplete(() => _enemy.SetActive(false));
            });
        transform.LookAt(target);
    }
}