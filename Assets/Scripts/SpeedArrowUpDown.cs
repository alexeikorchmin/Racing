using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeedArrowUpDown : MonoBehaviour
{
    private Image image;
    private Color color;

    private void Awake()
    {
        CarMovementController.OnSpeedIncreased += ShowSpeedArrow;
        image = GetComponent<Image>();
        color = image.color;
        color.a = 0f;
        image.color = color;
    }

    private void ShowSpeedArrow(bool flag)
    {
        if (!flag)
        {
            transform.Rotate(0f, 0f, 180f);
        }

        color.a = 1f;
        image.color = color;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        for (float i = 1f; i >= 0; i -= 0.2f)
        {
            color.a = i;
            image.color = color;
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnDestroy()
    {
        CarMovementController.OnSpeedIncreased -= ShowSpeedArrow;
    }
}
