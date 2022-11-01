using System.Collections.Generic;
using UnityEngine;

public class ReduceVolumeOnCloseDistance : MonoBehaviour
{
    [SerializeField] private List<AudioSource> audioSourcesToReduce;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private float distance = 100f;

    private bool isClose;

    private void Update()
    {
        CheckTheDistance();
    }

    private void CheckTheDistance()
    {
        if (Vector3.Distance(transform.position, cameraObject.transform.position) <= distance)
        {
            if (isClose) { return; }

            isClose = true;
            SoundManagerGlobal.Instance.ReduceVolumeOnCloseDistance(audioSourcesToReduce, true);
        }
        else
        {
            if (!isClose) { return; }

            isClose = false;
            SoundManagerGlobal.Instance.ReduceVolumeOnCloseDistance(audioSourcesToReduce, false);
        }
    }
}