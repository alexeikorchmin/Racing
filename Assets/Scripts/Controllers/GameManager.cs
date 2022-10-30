using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static BackgroundShift;

public class GameManager : MonoBehaviour
{
    public static event Action<bool> OnCanMove;
    public static event Action<MenuSprites> OnMenuSpriteChange;

    [SerializeField] private AdManager adManager;    
    [SerializeField] private EnergySystem energyManager;
    
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button watchVideoButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button closeSettingsButton;
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas playModeCanvas;

    private void Awake()
    {
        CarMovementController.OnObstacleBumped += OnObstacleBumpedHandler;
        AdManager.OnAdFinished += OnAdFinishedHandler;
        Init();

        playButton.onClick.AddListener(PlayGame);
        exitButton.onClick.AddListener(ExitGame);
        pauseButton.onClick.AddListener(PauseGame);
        playAgainButton.onClick.AddListener(PlayAgain);
        continueButton.onClick.AddListener(ContinueGame);
        settingButton.onClick.AddListener(delegate { OpenSettings(true); });
        closeSettingsButton.onClick.AddListener(delegate { OpenSettings(false); });
        watchVideoButton.onClick.AddListener(WatchVideo);
    }

    private void Init()
    {
        OnMenuSpriteChange?.Invoke(MenuSprites.MainMenu);
        playButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        watchVideoButton.gameObject.SetActive(false);
        playModeCanvas.enabled = false;
    }

    private void StartGame(bool enableMenuCanvas, bool enablePlaymodeCanvas, bool canMove)
    {
        menuCanvas.enabled = enableMenuCanvas;
        playModeCanvas.enabled = enablePlaymodeCanvas;
        OnCanMove?.Invoke(canMove);
    }

    private void PlayGame()
    {
        if (!energyManager.CheckIsEnoughEnergy()) { return; }

        StartGame(false, true, true);
        playButton.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(true);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void PauseGame()
    {
        StartGame(true, false, false);
        continueButton.gameObject.SetActive(true);
        OnMenuSpriteChange?.Invoke(MenuSprites.PauseMenu);
    }

    private void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    private void ContinueGame()
    {
        StartGame(false, true, true);
    }

    private void OpenSettings(bool isPanelOpen)
    {
        OnMenuSpriteChange?.Invoke(MenuSprites.SettingMenu);
        settingsPanel.SetActive(isPanelOpen);
    }

    private void WatchVideo()
    {
        Debug.Log("Video Played");
        adManager.ShowAd();
        watchVideoButton.interactable = false;
    }

    private void OnObstacleBumpedHandler()
    {
        PauseGame();
        continueButton.gameObject.SetActive(false);
        watchVideoButton.gameObject.SetActive(true);
    }

    private void OnAdFinishedHandler()
    {
        Debug.Log("Video Finished");
        ContinueGame();
        continueButton.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        CarMovementController.OnObstacleBumped -= OnObstacleBumpedHandler;
        AdManager.OnAdFinished -= OnAdFinishedHandler;
    }
}