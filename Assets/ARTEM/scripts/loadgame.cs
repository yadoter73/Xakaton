using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadingScreen : MonoBehaviour
{
    [Header("Настройки загрузки")]
    [Tooltip("Точное название сцены главного меню, как в Build Settings")]
    public string mainMenuSceneName = "MainMenu";

    [Header("Компоненты")]
    public VideoPlayer videoPlayer;

    private AsyncOperation asyncLoad;
    private bool isVideoFinished = false;

    void Start()
    {
        // 1. Настраиваем видео
        if (videoPlayer != null)
        {
            // Подписываемся на событие, которое сработает, когда видео закончится
            videoPlayer.loopPointReached += OnVideoFinished;
            videoPlayer.Play();
        }
        else
        {
            // Если видеоплеера нет, считаем, что видео "закончилось" сразу
            isVideoFinished = true;
        }

        // 2. Запускаем фоновую загрузку игры
        StartCoroutine(LoadSceneAsync());
    }

    // Этот метод вызовется автоматически, когда видеоплеер дойдет до конца ролика
    void OnVideoFinished(VideoPlayer vp)
    {
        isVideoFinished = true;
    }

    IEnumerator LoadSceneAsync()
    {
        // Начинаем асинхронную загрузку сцены
        asyncLoad = SceneManager.LoadSceneAsync(mainMenuSceneName);

        // Запрещаем Unity автоматически переключать сцену, когда она загрузится
        asyncLoad.allowSceneActivation = false;

        // Ждем выполнения двух условий: 
        // 1. Сцена загрузилась (в Unity прогресс останавливается на 0.9f)
        // 2. Флаг isVideoFinished стал true (видео закончилось)
        while (asyncLoad.progress < 0.9f || !isVideoFinished)
        {
            yield return null; // Ждем следующий кадр
        }

        // Как только оба условия выполнены — разрешаем переход!
        asyncLoad.allowSceneActivation = true;
    }
}