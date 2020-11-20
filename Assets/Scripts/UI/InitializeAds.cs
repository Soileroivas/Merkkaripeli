using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour
{
    string gameId = "3905233";

    void Start()
    {
        Advertisement.Initialize(gameId);

        //print(Advertisement.isInitialized);
    }

    public void ShowADS()
    {
        if (Advertisement.IsReady())
            Advertisement.Show();
        else
            Debug.LogError("Ad not ready");
    }
}
