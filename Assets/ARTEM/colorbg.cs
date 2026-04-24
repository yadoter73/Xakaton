using UnityEngine;

public class colorbg : MonoBehaviour
{
    [SerializeField] private Color targetColor = Color.white;
    private static readonly int TintProperty = Shader.PropertyToID("_Tint");

    public void ChangeSkyColor()
    {
        RenderSettings.skybox.SetColor(TintProperty, targetColor);
    }
}