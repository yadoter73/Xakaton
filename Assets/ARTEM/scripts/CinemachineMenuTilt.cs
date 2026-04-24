using System.Collections;
using UnityEngine;

public class CinemachineMenuOffset : MonoBehaviour
{
    public float moveRangeX = 1f;
    public float rotationRangeY = 5f;
    public float smoothSpeed = 2f;

    private IEnumerator Start()
    {
        Vector3 startPos = transform.localPosition;
        Quaternion startRot = transform.localRotation;

        while (true)
        {
            float m = (Input.mousePosition.x / Screen.width) * 2f - 1f;
            float t = Time.deltaTime * smoothSpeed;

            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos + Vector3.right * (m * moveRangeX), t);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, startRot * Quaternion.Euler(0, -m * rotationRangeY, 0), t);

            yield return null;
        }
    }
}