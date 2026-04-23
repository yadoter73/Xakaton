using UnityEngine;

public class LightToggle : MonoBehaviour
{
    private Light myLight;

    void Start()
    {
        
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
           
            myLight.enabled = !myLight.enabled;
        }
    }
}
