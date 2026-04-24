using UnityEngine;
using Cysharp.Threading.Tasks;
using PrimeTween;
using System.Threading;
using System;
using KinematicCharacterController;

public class PushTrigger : MonoBehaviour
{
    [SerializeField] private float _delayBeforePush = 0.2f;
    [SerializeField] private float _pushDistance = 3f;
    [SerializeField] private float _pushDuration = 0.5f;
    [SerializeField] private Transform _player;

    private CancellationTokenSource _cts;
    private Tween _activeTween;
    private KinematicCharacterMotor _motor;

    private Vector3 _entryPosition;

    private void Start()
    {
        _motor = _player.GetComponent<KinematicCharacterMotor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _entryPosition = other.transform.position;

            CancelPush();
            _cts = new CancellationTokenSource();
            Push(other.transform, _cts.Token).Forget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CancelPush();
        }
    }

    private async UniTaskVoid Push(Transform target, CancellationToken token)
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_delayBeforePush), cancellationToken: token);

            Vector3 pushDirection = (_entryPosition - transform.position).normalized;
            pushDirection.y = 0;

            Vector3 targetPosition = _entryPosition + (pushDirection * _pushDistance);

            _motor.enabled = false;

            _activeTween = Tween.Position(target, targetPosition, _pushDuration, Ease.OutQuad);

            await _activeTween;

            FinishPush(targetPosition);
        }
        catch (OperationCanceledException)
        {
        }
    }

    private void FinishPush(Vector3 finalPos)
    {
        _motor.enabled = true;
        _motor.SetPosition(finalPos);
    }

    private void CancelPush()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;

        if (_activeTween.isAlive)
        {
            _activeTween.Stop();
        }
        if (!_motor.enabled)
        {
            _motor.enabled = true;
        }
    }

    private void OnDestroy()
    {
        CancelPush();
    }
}