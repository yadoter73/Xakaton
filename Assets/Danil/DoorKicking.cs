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
	private bool _interactable = true;

	private void Start()
	{
		startY = legUI.transform.position.y;
		_anim = GetComponent<Animator>();
		_kickSound = GetComponent<AudioSource>();
	}
	public string GetDescription()
	{
		if (_interactable) return "E, ÷òîáû ÂÛÁÈÒÜ ÄÂÅÐÜ";
		return "";	}

	public void Interact(int id)
	{
		if (_interactable) Kick();
	}

	void Kick()
	{
		PlayLegAnimation();
		_anim.SetBool("Kick", true);
		_kickSound.Play();
		if (_sceneTransition != null)
		{
			_sceneTransition.StartTransition();
		}
		_interactable = false;
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
