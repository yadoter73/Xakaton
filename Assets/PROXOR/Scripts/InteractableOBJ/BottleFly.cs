using UnityEngine;
public class BottleFly : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _bottlePrefab;
    public string GetDescription() => "Pick Up!!!";

    public void Interact(int interactionType)
    {
        ThrowController thrower = FindObjectOfType<ThrowController>();
        thrower.Pickup(_bottlePrefab);
        gameObject.SetActive(false);
    }
}