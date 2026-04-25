using UnityEngine;
using PrimeTween;

public class DoorKicking : MonoBehaviour, IInteractable
{
	[SerializeField] RectTransform legUI;
	[SerializeField] float legMoveY = -300f;
	[SerializeField] float _animationDuration = 0.15f;
	[SerializeField] AudioSource _kickSound;
	[SerializeField] scenetoo _sceneTransition;

	private Animator _anim;
	private float startY;

	private void Start()
	{
		startY = legUI.transform.position.y;
		_anim = GetComponent<Animator>();
		_kickSound = GetComponent<AudioSource>();
	}
	public string GetDescription()
	{
		return "E to KNOCK DOWN THAT DOOR";
	}

	public void Interact(int id)
	{
		Kick();
	}

	void Kick()
	{
		PlayLegAnimation();
		_kickSound.Play();
		_anim.Play("DoorKick");
		_sceneTransition.StartTransition();
	}

	void PlayLegAnimation()
	{
		legUI.gameObject.SetActive(true);

		Sequence.Create()
			.Chain(Tween.UIAnchoredPositionY(legUI, legMoveY, _animationDuration, Ease.OutQuad))
			.Chain(Tween.UIAnchoredPositionY(legUI, startY, _animationDuration, Ease.InQuad))
			.OnComplete(() => {
				legUI.gameObject.SetActive(false);
			});
	}
}
