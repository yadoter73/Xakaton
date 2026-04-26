using UnityEngine;
using PrimeTween;
public class CorridorSmall : MonoBehaviour
{
    [SerializeField] GameObject[] _walls;
    [SerializeField] GameObject _text;

    [SerializeField] private float _moveDuration = 10f;
    [SerializeField] private float _moveStrength = 1f;

    private bool _isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_isTriggered || !other.CompareTag("Player")) return;

        _isTriggered = true;
        _text.SetActive(true);

        Tween.Delay(1.5f).OnComplete(() => Walls());

    }
    void Walls()
    {
        Tween.Delay(1.5f).OnComplete(() => _text.SetActive(false));
        Vector3 centerPosition = transform.position;

        foreach (GameObject wall in _walls)
        {
            Vector3 targetPosition = Vector3.Lerp(wall.transform.position, centerPosition, _moveStrength);
            Tween.Position(wall.transform, targetPosition, _moveDuration, Ease.InOutSine);
        }
    }
}
