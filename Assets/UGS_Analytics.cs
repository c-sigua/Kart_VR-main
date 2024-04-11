using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;


public class UGS_Analytics : MonoBehaviour
{
    private bool isCompletionist = false;
    private bool isResilient = false;
    private bool isBold = false;
    

    async void Start()
    {

        try
        {
            await UnityServices.InitializeAsync();
            if (ConsentForm.dataConsent == true)
            {
                GiveConsent();
                PlayTimeCustomEvent();
                if (KartGame.UI.LoadSceneButton.playedLevels.Contains("Level_Medium") && KartGame.UI.LoadSceneButton.playedLevels.Contains("Level_Hard") && KartGame.UI.LoadSceneButton.playedLevels.Contains("Level_Easy")) // checks if every level has been completed
                {
                    isCompletionist = true;
                }
                if ((int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Easy"] > 1 || (int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Medium"] > 1 || (int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Hard"] > 1)
                {
                    isResilient = true;
                }
                if ((int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Easy"] == 0 && (int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Medium"] == 0 && (int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Easy"] > 1)
                {
                    isBold = true;
                }
            }

            
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void PlayTimeCustomEvent()
    {
        
        float userTimeElapsed = TimeManager.timeElapsed;

        // Define Custom Parameters

        Debug.Log(userTimeElapsed);
        Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "playTime", userTimeElapsed}
            };

        // The ‘timeElapsed’ event will get cached locally
        //and sent during the next scheduled upload, within 1 minute
        AnalyticsService.Instance.CustomData("timeElapsed", parameters);

        // You can call Events.Flush() to send the event immediately
       // AnalyticsService.Instance.Flush();

    }

    public void GiveConsent()
    {
        // Call if consent has been given by the user
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }

    public void OnApplicationQuit() //used to send user behavior data once the player exits the game, so that it does not report multiple times
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "isCompletionist", isCompletionist},
                { "isResilient", isResilient },
                { "isBold", isBold}
            };

        AnalyticsService.Instance.CustomData("playerBehavior", parameters);

    }
}