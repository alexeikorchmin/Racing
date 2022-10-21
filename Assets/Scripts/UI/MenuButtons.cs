using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public static Action<bool> OnCanMove;

    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button continueButton;

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
    }

    private void Init()
    {
        playModeCanvas.enabled = false;
        continueButton.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
    }

    private void PlayGame()
    {
        menuCanvas.enabled = false;
        playModeCanvas.enabled = true;
        OnCanMove?.Invoke(true);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void PauseGame()
    {
        menuCanvas.enabled = true;
        playAgainButton.gameObject.SetActive(true);
        OnCanMove?.Invoke(false);
    }

    private void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    private void ContinueGame()
    {
        AdManager.Instance.ShowAd();
        continueButton.interactable = false;
    }

    private void OnObstacleBumpedHandler()
    {
        PauseGame();
        playButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
    }

    private void OnAdFinishedHandler()
    {
        PlayGame();
    }

    private void OnDestroy()
    {
        CarMovementController.OnObstacleBumped -= OnObstacleBumpedHandler;
        AdManager.OnAdFinished -= OnAdFinishedHandler;
    }
}