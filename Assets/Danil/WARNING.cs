using TMPro;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] private GameObject[] uiPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiPanel[0].SetActive(true);
            uiPanel[1].SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiPanel[0].SetActive(false);
            uiPanel[1].SetActive(false);
        }
    }
}