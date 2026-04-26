using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PrimeTween;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;
    public string sceneName;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void StartFadeAndLoad()
    {
        fadeImage.raycastTarget = true;
        Tween.Alpha(fadeImage, 1f, 1f).OnComplete(() => SceneManager.LoadScene(sceneName));
    }
}
