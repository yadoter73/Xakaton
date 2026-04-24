using TMPro;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI myText;

    private void Start() => Toggle(false);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Toggle(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) Toggle(false);
    }

    private void Toggle(bool state)
    {
        if (uiPanel) uiPanel.SetActive(state);
        if (myText) myText.gameObject.SetActive(state);
    }
}