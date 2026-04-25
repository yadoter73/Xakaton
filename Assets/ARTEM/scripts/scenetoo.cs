using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PrimeTween;

public class scenetoo : MonoBehaviour
{
	[SerializeField] Image fadeImage;
	[SerializeField] string sceneToLoad;
	[SerializeField] float fadeDuration = 0.5f;

	public void StartTransition()
	{
		fadeImage.gameObject.SetActive(true);
		Sequence.Create()
			.ChainDelay(1f)
			.Chain(Tween.Alpha(fadeImage, 1f, fadeDuration))
			.OnComplete(() => SceneManager.LoadScene(sceneToLoad));
	}
}

