using UnityEngine;
using KinematicCharacterController.Examples;
using Cysharp.Threading.Tasks;
using System.Threading;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _drainRate = 20f;
    [SerializeField] private float _regenRate = 15f;
    [SerializeField] private float _regenDelay = 4f;

    [SerializeField] private float _currentStamina;
    public bool isExhausted;
    public float CurrentStamina => _currentStamina;
    public float MaxStamina => _maxStamina;

    private ExampleCharacterController _controller;
    private CancellationTokenSource _cts;

    void Awake()
    {
        _controller = GetComponent<ExampleCharacterController>();
        _currentStamina = _maxStamina;
    }

    void OnEnable()
    {
        _cts = new CancellationTokenSource();
        StaminaLoop(_cts.Token).Forget();
    }

    void OnDisable()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private async UniTaskVoid StaminaLoop(CancellationToken token)
    {
        float lastUsedTime = -_regenDelay;

        while (!token.IsCancellationRequested)
        {
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);

            bool isSprinting = GetIsSprinting();

            if (isSprinting)
            {
                _currentStamina -= _drainRate * Time.fixedDeltaTime;
                lastUsedTime = Time.time;
            }
            else if (_currentStamina < _maxStamina && Time.time > lastUsedTime + _regenDelay)
            {
                _currentStamina += _regenRate * Time.fixedDeltaTime;
            }

            _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);

            if (_currentStamina <= 0)
                isExhausted = true;

            if (isExhausted && _currentStamina > _maxStamina * 0.2f)
                isExhausted = false;
        }
    }

    private bool GetIsSprinting()
    {
        return _controller.IsSprintingActual && !isExhausted;
    }
}