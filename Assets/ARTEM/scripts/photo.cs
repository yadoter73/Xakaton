using UnityEngine;
using UnityEngine.EventSystems;
using PrimeTween;

public class MenuImageItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MenuScroller scroller;
    public float scaleFactor = 1.2f;
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip exitSound;

    Vector3 baseScale;
    Tween scaleTween;

    void Awake() => baseScale = transform.localScale;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound(hoverSound);
        scaleTween.Stop();
        scaleTween = Tween.Scale(transform, baseScale * scaleFactor, 0.2f, Ease.OutQuad);
        scroller.CenterOn(transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlaySound(exitSound);
        scaleTween.Stop();
        scaleTween = Tween.Scale(transform, baseScale, 0.2f, Ease.OutQuad);
        scroller.Resume();
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource && clip) audioSource.PlayOneShot(clip);
    }
}