using UnityEngine;
using UnityEngine.SceneManagement;
using PrimeTween;

public class SceneToo : MonoBehaviour
{
    [SerializeField] CanvasGroup fade;
    [SerializeField] string scena;
    [SerializeField] float duration = 0.5f;

    public void Transition()
    {
        Tween.Alpha(fade, 1f, duration)
            .OnComplete(() => SceneManager.LoadScene(scena));
    }
}