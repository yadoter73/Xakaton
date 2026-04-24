using TMPro; 
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI myText;

    void Start()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
           
            if (myText != null) myText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiPanel.SetActive(true);
            if (myText != null) myText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiPanel.SetActive(false);
            if (myText != null) myText.gameObject.SetActive(false);
        }
    }
}
