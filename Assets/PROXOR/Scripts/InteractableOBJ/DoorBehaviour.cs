using Cysharp.Threading.Tasks;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour , IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private PlayerInteraction _playerInteraction;

    private bool _locked;
    private Animator _animator;
    public bool IsOpen => _animator.GetBool("IsOpen");
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void Interact(int id)
    {
        if (_id != id)
        {
            _locked = true;
            return;
        }
        _locked = false;
        _animator.SetBool("IsOpen", true);
        UniTask.WaitForSeconds(5).ContinueWith(() => _animator.SetBool("IsOpen", false)).Forget();
    }

    public string GetDescription()
    {
        return "E to knock down the door";
    }
}
