using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class AnimationPistol : MonoBehaviour
{
    [Header("Objects")]
    public RectTransform handParent;
    public RectTransform pistolObject;

    [Header("Settings")]
    public float moveSpeed = 2500f;
    public float downDistance = 1200f;
    public float delay = 0.2f;

    private Vector3 handStartPos;
    private Vector3 pistolStartPos;

    private bool busy = false;

    void Start()
    {
        handStartPos = handParent.anchoredPosition;
        pistolStartPos = pistolObject.anchoredPosition;

        // ïðÿ÷åì ïèñòîëåò âíèç
        pistolObject.anchoredPosition =
            pistolStartPos + Vector3.down * downDistance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && !busy)
        {
            StartCoroutine(EquipPistol());
        }
    }

    IEnumerator EquipPistol()
    {
        busy = true;

        Vector3 handDown =
            handStartPos + Vector3.down * downDistance;

        // ÐÓÊÀ ÂÍÈÇ
        while (Vector3.Distance(handParent.anchoredPosition, handDown) > 1f)
        {
            handParent.anchoredPosition = Vector3.MoveTowards(
                handParent.anchoredPosition,
                handDown,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        handParent.anchoredPosition = handDown;

        yield return new WaitForSeconds(delay);

        // ÏÈÑÒÎËÅÒ ÂÂÅÐÕ
        while (Vector3.Distance(pistolObject.anchoredPosition, pistolStartPos) > 1f)
        {
            pistolObject.anchoredPosition = Vector3.MoveTowards(
                pistolObject.anchoredPosition,
                pistolStartPos,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        pistolObject.anchoredPosition = pistolStartPos;

        busy = false;
    }
}