using Cysharp.Threading.Tasks;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private PlayerInteraction _playerInteraction;

    private bool _isOpen;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _isOpen = _animator.GetBool("IsOpen");
    }
    public void Interact(int id)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) return;

        if (_id != id) return;

        _isOpen = !_isOpen;
        _animator.SetBool("IsOpen", _isOpen);
    }
    public string GetDescription()
    {
        return _isOpen ? "Press E to close" : "Press E to open";


    }
}
