using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Light))]
public class RealisticFlicker : MonoBehaviour
{
    public float minIntensity = 0f;
    public float maxIntensity = 1f;
    public float minSpeed = 0.05f;
    public float maxSpeed = 0.15f;

    private Light _light;
    private float _targetIntensity;
    private float _currentSpeed;

    void Start()
    {
        _light = GetComponent<Light>();
        StartCoroutine(FlickerLogic());
    }

    IEnumerator FlickerLogic()
    {
        while (true)
        {
            _targetIntensity = Random.Range(minIntensity, maxIntensity);
            _currentSpeed = Random.Range(minSpeed, maxSpeed);

            float startTime = Time.time;
            float startIntensity = _light.intensity;

            while (Time.time - startTime < _currentSpeed)
            {
                _light.intensity = Mathf.Lerp(startIntensity, _targetIntensity, (Time.time - startTime) / _currentSpeed);
                yield return null;
            }

            _light.intensity = _targetIntensity;
        }
    }
}