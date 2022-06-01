using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playAgainButton;

    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas playModeCanvas;

    public static Action<bool> OnCanMove;

    private void Awake()
    {
        CarMovementController.OnObstacleBumped += WhenCarBumpedMenu;

        playModeCanvas.enabled = false;

        //PauseGame();
        playAgainButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);

        playButton.onClick.AddListener(PlayGame);
        exitButton.onClick.AddListener(ExitGame);
        pauseButton.onClick.AddListener(PauseGame);
        playAgainButton.onClick.AddListener(PlayAgain);
    }

    private void PlayGame()
    {
        menuCanvas.enabled = false;
        playModeCanvas.enabled = true;
        //Time.timeScale = 1f;
        OnCanMove?.Invoke(true);
    }

    private void ExitGame()
    {
        print("Good Bye");
        //Application.Quit();
    }

    private void PauseGame()
    {
        //Time.timeScale = 0f;
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

    private void OnDestroy()
    {
        CarMovementController.OnObstacleBumped -= WhenCarBumpedMenu;
    }
}
