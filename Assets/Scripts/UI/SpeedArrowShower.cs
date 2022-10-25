using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SpeedArrowShower : MonoBehaviour
{
    private Quaternion startRotation;
    private float timeDelay = 1f;
    private Tween scaleTween;

    private void Awake()
    {
        CarMovementController.OnSpeedIncreased += ShowSpeedArrow;
        Init();
    }

    private void Init()
    {
        startRotation = transform.rotation;
        transform.localScale = Vector3.zero;
    }

    private void ShowSpeedArrow(bool flag)
    {
        if (flag)
        {
            transform.rotation = startRotation;
        }
        else
        {
            transform.Rotate(0f, 0f, 180f);
        }

        StartCoroutine(ArrowScaling());
    }

    private IEnumerator ArrowScaling()
    {
        scaleTween.Kill();
        scaleTween = transform.DOScale(2f, timeDelay / 2);
        yield return new WaitForSeconds(timeDelay);
        scaleTween = transform.DOScale(0f, timeDelay / 2);
    }

    private void OnDestroy()
    {
        CarMovementController.OnSpeedIncreased -= ShowSpeedArrow;
    }
}