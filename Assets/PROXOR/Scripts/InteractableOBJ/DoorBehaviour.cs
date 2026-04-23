using Cysharp.Threading.Tasks;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private int _id;

    private bool _locked;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void Interact(int id)
    {
        if (_id != id)
        {
            _locked = true;
        }
        if (_locked)
        {
            _animator.SetBool("IsOpen", true);
            UniTask.WaitForSeconds(4).ContinueWith(() => _animator.SetBool("IsOpen", false)).Forget();
        }
        
    }
    public string GetState()
    {
        return _locked ? "<color=red>Door's locked</color>  " :
            "<color=green>Door's opened</color>";
    }
    public string GetDescription()
    {
        return "Press F to open the door";
    }
}
