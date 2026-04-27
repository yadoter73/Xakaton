using UnityEngine;
using PrimeTween;
public class BottleMovement : MonoBehaviour
{
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] GameObject _text;
    [SerializeField] GameObject _enemy;
    public void SetTarget(Transform target)
    {
        if (target == null) return;
        _text.SetActive(false);
        Tween.Position(transform,target.position, _duration, Ease.InQuad)
            .OnComplete(() =>
            {
                _enemy.SetActive(false);
                Destroy(gameObject);
            });
        transform.LookAt(target);
    }
}