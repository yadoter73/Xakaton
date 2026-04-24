using UnityEngine;


public class WARNING : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel; 

    void Start()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false); 
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            if (uiPanel != null)
                uiPanel.SetActive(true); 
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        { 
            if (uiPanel != null)
                uiPanel.SetActive(false);
        }
    }
}
    