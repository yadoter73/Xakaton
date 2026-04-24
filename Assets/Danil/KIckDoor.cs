using UnityEngine;

public class KickDoor : MonoBehaviour
{
    public float kickForce = 450f;
    public float upwardForce = 30f;
    public float kickRange = 5f;
    public float collisionIgnoreTime = 0.2f;
    public AudioClip kickSound;  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            TryKick();
    }

    void TryKick()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, kickRange))
        {
            Rigidbody rb = hit.rigidbody;
            if (rb != null)
            {
                
                if (kickSound != null)
                    AudioSource.PlayClipAtPoint(kickSound, hit.point);

                foreach (Joint j in rb.GetComponents<Joint>())
                    Destroy(j);

                rb.isKinematic = false;
                rb.constraints = RigidbodyConstraints.None;

                Collider playerCol = GetComponent<Collider>();
                if (playerCol != null)
                {
                    foreach (Collider doorCol in rb.GetComponentsInChildren<Collider>())
                        Physics.IgnoreCollision(doorCol, playerCol, true);

                    StartCoroutine(RestoreCollision(rb, playerCol, collisionIgnoreTime));
                }

                Vector3 forwardDir = ray.direction;
                forwardDir.y += 0.4f;
                forwardDir.Normalize();

                Vector3 lowPoint = hit.collider.bounds.center;
                lowPoint.y = hit.collider.bounds.min.y + 0.1f;

                rb.angularDamping = 0f;
                rb.linearDamping = 0f;

                rb.AddForceAtPosition(forwardDir * kickForce, lowPoint, ForceMode.Impulse);
                rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
            }
        }
    }

    System.Collections.IEnumerator RestoreCollision(Rigidbody doorRb, Collider playerCol, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (doorRb != null && playerCol != null)
        {
            foreach (Collider doorCol in doorRb.GetComponentsInChildren<Collider>())
            {
                if (doorCol != null)
                    Physics.IgnoreCollision(doorCol, playerCol, false);
            }
        }
    }
}