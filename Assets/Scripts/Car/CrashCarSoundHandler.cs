using UnityEngine;

public class CrashCarSoundHandler : SoundManagerLocal
{
    [SerializeField] private AudioClip audioClip;

    protected override void Awake()
    {
        base.Awake();
        CarMovementController.OnObstacleBumped += OnObstacleBumpedHandler;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        CarMovementController.OnObstacleBumped -= OnObstacleBumpedHandler;
    }

    protected override void OnCanMoveHandler(bool canMove) { }

    private void OnObstacleBumpedHandler()
    {
        audioSource.PlayOneShot(audioClip);
    }
}