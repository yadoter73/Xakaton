using UnityEngine;
using PrimeTween;
using KinematicCharacterController;
using KinematicCharacterController.Examples;
public class RollerTable : MonoBehaviour , IInteractable
{
    [SerializeField] Transform _player;
    [SerializeField] Transform _startPos;
    [SerializeField] Transform _endPos;
    [SerializeField] AnimationCurve _curve;

    private KinematicCharacterMotor _motor;
    private ExampleCharacterController _controller;

    private void Start()
    {
        _controller = _player.GetComponent<ExampleCharacterController>();
        _motor = _player.GetComponent<KinematicCharacterMotor>();
    }
    public string GetDescription()
    {
        return "Press E to roll";
    }
    public void Interact(int id)
    {
        _motor.enabled = false;
        Tween.Position(_player, _startPos.position, _endPos.position, 1f, _curve)
            .OnComplete(() =>
        {
            _motor.enabled = true;
            _motor.SetPosition(_endPos.position);
        });
    }
}
