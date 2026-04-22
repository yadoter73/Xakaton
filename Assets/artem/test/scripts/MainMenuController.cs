using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PrimeTween;

public class CS2MenuManager : MonoBehaviour
{
    [Header("Аудио")]
    public AudioSource menuMusic;       // Музыка в меню
    public AudioSource loadingMusic;    // Музыка при загрузке (loop)
    public AudioSource finalMusic;      // Финальная песня (8 секунд)

    [Header("UI Панели (CanvasGroup)")]
    public CanvasGroup mainWindow;      // Главный экран
    public CanvasGroup playWindow;      // Окно "Играть"
    public CanvasGroup settingsWindow;  // Окно "Настройки"
    public CanvasGroup loadingScreen;   // Экран загрузки
    public GameObject pressSpaceText;   // Текст "Нажмите пробел"

    private bool isWaitingForSpace = false;
    private bool isLoadingFinished = false;

    private void Start()
    {
        // Инициализация аудио
        menuMusic.loop = true;
        loadingMusic.loop = true;
        menuMusic.volume = 1f;
        menuMusic.Play();

        // Прячем всё лишнее
        HidePanelInstant(playWindow);
        HidePanelInstant(settingsWindow);
        HidePanelInstant(loadingScreen);
        pressSpaceText.SetActive(false);
    }

    private void Update()
    {
        // Если мы на экране загрузки и нажали пробел
        if (isWaitingForSpace && Input.GetKeyDown(KeyCode.Space))
        {
            isWaitingForSpace = false;
            pressSpaceText.SetActive(false);

            // Запускаем процесс ожидания конца такта/трека
            StartCoroutine(WaitAndSwitchMusic());
        }
    }

    private System.Collections.IEnumerator WaitAndSwitchMusic()
    {
        // Вычисляем, сколько времени осталось до конца текущего проигрывания трека
        float remainingTime = loadingMusic.clip.length - loadingMusic.time;

        // Ждем ровно это время
        yield return new WaitForSeconds(remainingTime);

        // Мгновенное переключение
        loadingMusic.Stop();

        finalMusic.volume = 1f;
        finalMusic.Play();

        // Отсчитываем 8 секунд от начала финального трека
        Sequence.Create()
            .ChainDelay(8f)
            .ChainCallback(() =>
            {
                SceneManager.LoadSceneAsync("test3d");
            });
    }

    // --- НАВИГАЦИЯ ПО МЕНЮ ---

    public void OpenPlayWindow()
    {
        HidePanel(settingsWindow);
        ShowPanel(playWindow);
    }

    public void OpenSettingsWindow()
    {
        HidePanel(playWindow);
        ShowPanel(settingsWindow);
    }

    public void BackToMain()
    {
        HidePanel(playWindow);
        HidePanel(settingsWindow);
    }

    // --- ЛОГИКА ЗАГРУЗКИ (Нажатие на кнопку "ИГРАТЬ") ---

    public void StartGameLoad()
    {
        // Затухание музыки меню и включение музыки загрузки
        Tween.AudioVolume(menuMusic, 0f, 1f).OnComplete(() => menuMusic.Stop());

        loadingMusic.volume = 0f;
        loadingMusic.Play();
        Tween.AudioVolume(loadingMusic, 1f, 1f);

        // Показываем экран загрузки
        ShowPanel(loadingScreen);

        // Эмуляция загрузки (допустим, мы грузим данные 2 секунды, потом просим пробел)
        Sequence.Create()
            .ChainDelay(2f)
            .ChainCallback(() =>
            {
                isWaitingForSpace = true;
                pressSpaceText.SetActive(true);
                // Мигание текста "Нажмите пробел" через PrimeTween
                Tween.Alpha(pressSpaceText.GetComponent<CanvasGroup>(), 0f, 1f, 0.5f, cycles: -1, cycleMode: CycleMode.Yoyo);
            });
    }

    private void StartFinalLoadingSequence()
    {
        pressSpaceText.SetActive(false);
        // Останавливаем анимацию мигания текста
        Tween.StopAll(pressSpaceText.GetComponent<CanvasGroup>());

        // Резкое переключение музыки: 
        // моментально глушим загрузочную и сразу включаем финальную
        loadingMusic.Stop();

        finalMusic.volume = 1f;
        finalMusic.Play();

        // Ждем 8 секунд после старта финальной песни и загружаем игровую сцену
        Sequence.Create()
            .ChainDelay(8f)
            .ChainCallback(() =>
            {
                // Замени "GameScene" на название твоей сцены
                SceneManager.LoadSceneAsync("test3d");
            });
    }
    // --- ВСПОМОГАТЕЛЬНЫЕ ФУНКЦИИ АНИМАЦИИ ---

    private void ShowPanel(CanvasGroup panel)
    {
        panel.gameObject.SetActive(true);
        panel.blocksRaycasts = true;
        Tween.Alpha(panel, 1f, 0.3f, Ease.OutQuad);
    }

    private void HidePanel(CanvasGroup panel)
    {
        panel.blocksRaycasts = false;
        Tween.Alpha(panel, 0f, 0.2f, Ease.InQuad).OnComplete(() => panel.gameObject.SetActive(false));
    }

    private void HidePanelInstant(CanvasGroup panel)
    {
        panel.alpha = 0f;
        panel.blocksRaycasts = false;
        panel.gameObject.SetActive(false);
    }
}