using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MenuButtons : MonoBehaviour
{
    public static Action<bool> OnCanMove;

    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playAgainButton;

    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas playModeCanvas;

    private void Awake()
    {
        CarMovementController.OnObstacleBumped += WhenCarBumpedMenu;

        Init();

        playButton.onClick.AddListener(PlayGame);
        exitButton.onClick.AddListener(ExitGame);
        pauseButton.onClick.AddListener(PauseGame);
        playAgainButton.onClick.AddListener(PlayAgain);
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

    private void WhenCarBumpedMenu()
    {
        PauseGame();
        playButton.gameObject.SetActive(false);
    }

    private void Init()
    {
        playModeCanvas.enabled = false;
        playAgainButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        CarMovementController.OnObstacleBumped -= WhenCarBumpedMenu;
    }
}