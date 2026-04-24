using System.Collections;
using UnityEngine;
using PrimeTween;
using KinematicCharacterController.Examples;

public class HandBobAnimation : MonoBehaviour
{
    [SerializeField] private ExampleCharacterController player;
    [SerializeField] private RectTransform hand;
    [SerializeField] private Vector2 bobOffset = new Vector2(15f, -20f);
    [SerializeField] private float walkDuration = 0.35f;
    [SerializeField] private float sprintDuration = 0.2f;

    private Vector2 startPos;

    private void Start()
    {
        startPos = hand.anchoredPosition;
        StartCoroutine(BobRoutine());
    }

    private IEnumerator BobRoutine()
    {
        while (true)
        {
            bool isMoving = player.Motor.Velocity.sqrMagnitude > 0.1f && player.Motor.GroundingStatus.IsStableOnGround;

            if (isMoving)
            {
                float duration = player.IsSprintingActual ? sprintDuration : walkDuration;

                yield return Tween.UIAnchoredPosition(hand, startPos + bobOffset, duration, Ease.InOutSine).ToYieldInstruction();
                yield return Tween.UIAnchoredPosition(hand, startPos, duration, Ease.InOutSine).ToYieldInstruction();
            }
            else
            {
                if (hand.anchoredPosition != startPos)
                {
                    yield return Tween.UIAnchoredPosition(hand, startPos, walkDuration, Ease.OutSine).ToYieldInstruction();
                }
                yield return null;
            }
        }
    }
}