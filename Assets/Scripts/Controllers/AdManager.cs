using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static event Action OnAdFinished;

    private AdManager Instance;
    private string rewardedPlacementId = "Rewarded_Android";
    //private string interstitialPlacementId = "Interstitial_Android";

#if UNITY_ANDROID
    private string gameId = "4983267";
    //#elif Unity_IOS
    //private string gameId = "4983266";
#endif

    public void ShowAd()
    {
        Advertisement.Load(rewardedPlacementId, this);
        Debug.Log("ShowAd - Advertisement.Load OK");
        Advertisement.Show(rewardedPlacementId, this);
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (Instance != null)
            Instance = this;

        Advertisement.Initialize(gameId, true, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("On Unity Ads Initialization OK");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"On Unity Ads Initialization FAIL {error} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"On Unity Ads Loaded OK: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"On Unity Ads Load FAIL: {placementId}: {error} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"On Unity Ads Show FAIL: {placementId}: {error} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"On Unity Ads Show Start OK: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"On Unity Ads Show Click OK: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.COMPLETED:
                Debug.Log($"placementID = {placementId} ");
                OnAdFinished?.Invoke();
                Debug.Log("Ad COMPLETED");
                break;
            case UnityAdsShowCompletionState.SKIPPED:
                Debug.Log("Ad SKIPPED");
                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                Debug.Log("Ad UNKNOWN");
                break;
        }
    }
}