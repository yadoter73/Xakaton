using UnityEngine;
using UnityEngine.SceneManagement;
using PrimeTween;

public class PauseManager : MonoBehaviour
{
    public RectTransform menuRect;
    public float animSpeed = 0.35f;
    private bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        menuRect.anchoredPosition = new Vector2(-1920f, menuRect.anchoredPosition.y);
        Tween.UIAnchoredPositionX(menuRect, 0f, animSpeed, Ease.OutQuad, useUnscaledTime: true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        Tween.UIAnchoredPositionX(menuRect, -1920f, animSpeed, Ease.InQuad, useUnscaledTime: true);
    }

    public void LoadNextScene(int sceneIndex)
    {
        isPaused = false;
        Time.timeScale = 1f;

        Sequence.Create(useUnscaledTime: true)
            .Chain(Tween.UIAnchoredPositionX(menuRect, 1920f, animSpeed, Ease.InQuad))
            .ChainDelay(1f)
            .OnComplete(() => SceneManager.LoadScene(sceneIndex));
    }
}