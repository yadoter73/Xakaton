using UnityEngine;
using UnityEngine.UI;

public class BottleFly : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _bottlePrefab;
    [SerializeField] Image _hand;
    [SerializeField] Sprite _bottle;
    public string GetDescription() => "Pick Up!!!";

    public void Interact(int interactionType)
    {

        ThrowController thrower = FindObjectOfType<ThrowController>();
        thrower.Pickup(_bottlePrefab);
        _hand.sprite = _bottle;
        gameObject.SetActive(false);
    }
}