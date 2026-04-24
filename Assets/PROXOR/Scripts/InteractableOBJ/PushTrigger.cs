using UnityEngine;
using Cysharp.Threading.Tasks; 
using PrimeTween;              
using System.Threading;
using System;
using KinematicCharacterController;

public class PushTrigger : MonoBehaviour
{
    [SerializeField] private float _delayBeforePush = 1.5f; 
    [SerializeField] private float _pushDistance = 3f;      
    [SerializeField] private float _pushDuration = 0.5f;
    [SerializeField] Transform _player;

    private CancellationTokenSource _cts;

    private Tween _activeTween;

    private KinematicCharacterMotor _motor;
    private void Start()
    {
        _motor = _player.GetComponent<KinematicCharacterMotor>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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

            Vector3 pushDirection = (target.position - transform.position).normalized;
            pushDirection.y = 0;
            Vector3 targetPosition = target.position + pushDirection * _pushDistance;
            _motor.enabled = false;
            _activeTween = Tween.Position(target,
                                          targetPosition,
                                          _pushDuration,
                                          Ease.OutQuad).OnComplete(() => { _motor.enabled = true; _motor.SetPosition(targetPosition); });

            await _activeTween;
        }
        catch (OperationCanceledException)
        {
            return;
        }

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
    }

    private void OnDestroy()
    {
        CancelPush();
    }
}