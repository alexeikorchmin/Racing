using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action<bool> OnCanMove;

    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button watchVideoButton;

    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas playModeCanvas;
    [SerializeField] private EnergySystem energyManager;

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
        watchVideoButton.onClick.AddListener(WatchVideo);
    }

    private void Init()
    {
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
        continueButton.gameObject.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void PauseGame()
    {
        StartGame(true, false, false);
    }

    private void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    private void ContinueGame()
    {
        StartGame(false, true, true);
    }

    private void WatchVideo()
    {
        AdManager.Instance.ShowAd();
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
        ContinueGame();
    }

    private void OnDestroy()
    {
        CarMovementController.OnObstacleBumped -= OnObstacleBumpedHandler;
        AdManager.OnAdFinished -= OnAdFinishedHandler;
    }
}