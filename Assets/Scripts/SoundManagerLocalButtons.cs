using UnityEngine;

public class SoundManagerLocalButtons : SoundManagerLocal
{
    [SerializeField] private AudioClip audioClip;

    private float timer = 0f;
    private bool isButtonPressed = false;

    protected override void Awake()
    {
        base.Awake();
        SteerLeftController.OnLeftButtonPressed += PlayOnClickDown;
        SteerRightController.OnRightButtonPressed += PlayOnClickDown;
    }

    private void Update()
    {
        if (isButtonPressed)
        {
            timer += Time.deltaTime;

            if (timer > 1f)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }
    }

    private void PlayOnClickDown(bool flag)
    {
        isButtonPressed = flag;

        if (!flag)
        {
            timer = 0f;
        }
    }

    protected override void GameIsPlayed(bool flag) { }

    private void OnDestroy()
    {
        SteerLeftController.OnLeftButtonPressed -= PlayOnClickDown;
        SteerRightController.OnRightButtonPressed -= PlayOnClickDown;
    }
}
