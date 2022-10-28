using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public static event Action OnAdFinished;
    public static AdManager Instance;

#if UNITY_ANDROID
    private string gameId = "4983267";
//#elif Unity_IOS
//  private string gameId = "4983266";
#endif

    public void ShowAd()
    {
        Advertisement.Show("Rewarded_Android");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError($"Unity Ads Error - {message}");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Unity Ad Started");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Unity Ad Ready");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
                OnAdFinished?.Invoke();
                Debug.Log("Ad Finished");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad Skipped");
                break;
            case ShowResult.Failed:
                Debug.Log("Ad Failed");
                break;
        }
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId);
        }
    }
}