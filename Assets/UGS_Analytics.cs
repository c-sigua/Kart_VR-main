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
                Debug.Log("data consent registered as true");

                GiveConsent();
                //PlayTimeCustomEvent();
                if (KartGame.UI.LoadSceneButton.playedLevels.Contains("Level_Medium") && KartGame.UI.LoadSceneButton.playedLevels.Contains("Level_Hard") && KartGame.UI.LoadSceneButton.playedLevels.Contains("Level_Easy")) // checks if every level has been completed
                {
                    isCompletionist = true;
                }
                if ((int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Easy"] > 1 || (int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Medium"] > 1 || (int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Hard"] > 1)
                {
                    isResilient = true;
                }
                if ((int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Easy"] == 0 && (int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Medium"] == 0 && (int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Hard"] > 0)
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

    public void PlayerBehaviorCustomEvent()
    {
        //Debug.Log(GameFlowManager.gamesWon.ToString() + "/" + GameFlowManager.gamesLost.ToString());
        Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "timesEasyPlayed", (int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Easy"]},
                { "timesMediumPlayed", (int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Medium"]},
                { "timesHardPlayed", (int)KartGame.UI.LoadSceneButton.timesLevelPlayed["Level_Hard"]},
                { "winLossRatio", GameFlowManager.gamesWon.ToString() + " / " + GameFlowManager.gamesLost.ToString()},
                { "isCompletionist", isCompletionist},
                { "isResilient", isResilient },
                { "isBold", isBold}
            };

        AnalyticsService.Instance.CustomData("playerBehavior", parameters);
    }

    public void PlayerDemographicsCustomEvent()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "age", SliderScript.userAge},
                { "gender", DropdownScript.userGender },
            };

        AnalyticsService.Instance.CustomData("playerDemographics", parameters);
    }

    public void GiveConsent()
    {
        // Call if consent has been given by the user
        AnalyticsService.Instance.StartDataCollection();
        //Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }

    public void OnClose()
    {
        if (ConsentForm.dataConsent == true)
        {
            PlayerDemographicsCustomEvent();
            //Debug.Log(SliderScript.userAge);
            //Debug.Log(DropdownScript.userGender);
            PlayerBehaviorCustomEvent();
            AnalyticsService.Instance.Flush();

        }
        else
        {
            Debug.Log("consent was not given, data not sent");
        }
    }

    public void OnApplicationQuit() //used to send user behavior data once the player exits the game, so that it does not report multiple times
    {
        if (ConsentForm.dataConsent == true)
        {
            PlayerDemographicsCustomEvent();
            //Debug.Log(SliderScript.userAge);
            //Debug.Log(DropdownScript.userGender);
            PlayerBehaviorCustomEvent();
            AnalyticsService.Instance.Flush();
          
        }
        else 
        {
            Debug.Log("consent was not given, data not sent");
        }
    }
}