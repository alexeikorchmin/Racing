using UnityEngine;
using UnityEngine.UI;

public class BackgroundShift : MonoBehaviour
{
    [SerializeField] private Sprite[] backgroundSprites;
    [SerializeField] private Image canvasImage;

    public enum MenuSprites
    {
        MainMenu = 0,
        PauseMenu = 1,
        SettingMenu = 2,
    }
    
    private void Awake()
    {
        GameManager.OnMenuSpriteChange += OnMenuSpriteChangeHandler;
    }

    private void OnDestroy()
    {
        GameManager.OnMenuSpriteChange -= OnMenuSpriteChangeHandler;
    }

    private void OnMenuSpriteChangeHandler(MenuSprites menuSprites)
    {
        canvasImage.sprite = backgroundSprites[(int)menuSprites];
    }
}