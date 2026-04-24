using UnityEngine;
using PrimeTween;

public class KickDoor : MonoBehaviour
{
    public float kickForce = 450f;
    public float upwardForce = 30f;
    public float kickRange = 5f;
    public float collisionIgnoreTime = 0.2f;
    public AudioClip kickSound;

    public RectTransform legUI;
    public float legMoveY = -300f;
    public float animationDuration = 0.15f;

    private bool isKicking;
    private Camera cam;
    private Collider playerCol;
    private float startY;

    void Awake()
    {
        cam = Camera.main;
        playerCol = GetComponent<Collider>();
        if (legUI) startY = legUI.anchoredPosition.y;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isKicking)
        {
            if (Physics.Raycast(cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out RaycastHit hit, kickRange))
            {
                if (hit.rigidbody != null)
                {
                    PerformKick(hit.rigidbody, hit);
                }
            }
        }
    }

    void PerformKick(Rigidbody rb, RaycastHit hit)
    {
        PlayLegAnimation();

        rb.constraints = RigidbodyConstraints.None;
        foreach (var j in rb.GetComponents<Joint>()) Destroy(j);
        rb.isKinematic = false;

        if (kickSound) AudioSource.PlayClipAtPoint(kickSound, hit.point);

        if (playerCol)
        {
            var doorCols = rb.GetComponentsInChildren<Collider>();
            foreach (var c in doorCols) Physics.IgnoreCollision(c, playerCol, true);

            Tween.Delay(collisionIgnoreTime).OnComplete(() => {
                if (rb && playerCol)
                    foreach (var c in doorCols)
                        if (c) Physics.IgnoreCollision(c, playerCol, false);
            });
        }

        Vector3 dir = cam.transform.forward;
        dir.y += 0.4f;

        Vector3 lowPoint = hit.collider.bounds.center;
        lowPoint.y = hit.collider.bounds.min.y + 0.1f;

        rb.angularDamping = 0f;
        rb.linearDamping = 0f;

        rb.AddForceAtPosition(dir.normalized * kickForce, lowPoint, ForceMode.Impulse);
        rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
    }

    void PlayLegAnimation()
    {
        if (!legUI) return;

        isKicking = true;
        legUI.gameObject.SetActive(true);

        Sequence.Create()
            .Chain(Tween.UIAnchoredPositionY(legUI, startY + legMoveY, animationDuration, Ease.OutQuad))
            .Chain(Tween.UIAnchoredPositionY(legUI, startY, animationDuration, Ease.InQuad))
            .OnComplete(() => {
                legUI.gameObject.SetActive(false);
                isKicking = false;
            });
    }
}