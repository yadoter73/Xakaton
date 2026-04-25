using UnityEngine;
using PrimeTween;
using Exploder.Utils;

public class KickExplosive : MonoBehaviour, IInteractable
{
    [SerializeField] RectTransform legUI;
    [SerializeField] float legMoveY = -300f;
    [SerializeField] float animationDuration = 0.15f;
    [SerializeField] float maxDistance = 3f;

    private AudioSource kickSound;
    private float startY;
    private Transform player;

    private void Start()
    {
        kickSound = GetComponent<AudioSource>();
        startY = legUI.anchoredPosition.y;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, player.position) <= maxDistance)
        {
            Debug.Log("jsfhgd");
            Interact(0);
        }
    }

    public string GetDescription() => "E, ÷òîáû ÐÀÇÁÈÒÜ";

    public void Interact(int id)
    {
        PlayLegAnimation();
        kickSound.Play();
        ExploderSingleton.ExploderInstance.ExplodeObject(gameObject);
    }

    private void PlayLegAnimation()
    {
        legUI.gameObject.SetActive(true);
        Sequence.Create()
            .Chain(Tween.UIAnchoredPositionY(legUI, legMoveY, animationDuration, Ease.OutQuad))
            .Chain(Tween.UIAnchoredPositionY(legUI, startY, animationDuration, Ease.InQuad))
            .OnComplete(() => legUI.gameObject.SetActive(false));
    }
}