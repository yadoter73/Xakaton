using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public MenuScreen mainMenu;
    public MenuScreen playMenu;
    public MenuScreen settingsMenu;
    public MenuScreen aboutMenu;

    public float tweenDuration = 0.5f;
    public float screenOffset = 1080f;

    private MenuScreen currentScreen;

    private void Start()
    {
        currentScreen = mainMenu;

        mainMenu.SetInstantPositionY(0f);
        playMenu.SetInstantPositionY(-screenOffset);
        settingsMenu.SetInstantPositionY(-screenOffset);
        aboutMenu.SetInstantPositionY(-screenOffset);
    }

    public void OpenPlay() => SwitchToMenu(playMenu);
    public void OpenSettings() => SwitchToMenu(settingsMenu);
    public void OpenAbout() => SwitchToMenu(aboutMenu);

    private void SwitchToMenu(MenuScreen target)
    {
        if (currentScreen == target) return;

        mainMenu.MoveToY(screenOffset, tweenDuration);
        target.MoveToY(0f, tweenDuration);

        currentScreen = target;
    }

    public void GoBack()
    {
        if (currentScreen == mainMenu) return;

        currentScreen.MoveToY(-screenOffset, tweenDuration);
        mainMenu.MoveToY(0f, tweenDuration);

        currentScreen = mainMenu;
    }
}