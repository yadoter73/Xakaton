using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BottleFly : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _bottlePrefab;
    [SerializeField] Image _hand;
    [SerializeField] Sprite _bottle;
    [SerializeField] TextMeshProUGUI _text;
    public string GetDescription() => "Бери!!!";

    public void Interact(int interactionType)
    {
        _text.gameObject.SetActive(true);
        _text.text = "(ЛКМ)Брось в охранника спереди!!!";
        ThrowController thrower = FindObjectOfType<ThrowController>();
        thrower.Pickup(_bottlePrefab);
        _hand.sprite = _bottle;
        gameObject.SetActive(false);
    }
}