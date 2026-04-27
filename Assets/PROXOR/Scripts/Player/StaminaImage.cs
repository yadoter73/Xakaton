using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private StaminaSystem staminaSystem;
    [SerializeField] private Image fillImage;

    private float lastFill = -1f;

    private void Update()
    {
        float currentFill = staminaSystem.CurrentStamina / staminaSystem.MaxStamina;

        if (Mathf.Abs(lastFill - currentFill) > 0.001f)
        {
            fillImage.fillAmount = currentFill;
            lastFill = currentFill;
        }
    }
}