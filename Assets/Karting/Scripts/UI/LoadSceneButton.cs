using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
        public string SceneName;
        public static HashSet<string> playedLevels = new HashSet<string>(); //set containing which scenes have been played
        public static Hashtable timesLevelPlayed = new Hashtable() //table containing each time a level has been played
        {
            {"Level_Easy", 0},
            {"Level_Medium", 0},
            {"Level_Hard", 0}
        };




        public void LoadTargetScene() 
        {
            SceneManager.LoadSceneAsync(SceneName);

            if (SceneName != "IntroMenu")
            {
                playedLevels.Add(SceneName);
                timesLevelPlayed[SceneName] = (int)timesLevelPlayed[SceneName] + 1;
               
                //reports each scene has been played
                //Debug.Log(String.Join(",", playedLevels));
                
                foreach (DictionaryEntry entry in timesLevelPlayed)
                {
                    //Reports the amount of times each level is played
                    //Debug.Log(entry.Key + " : " + entry.Value);
                }

            }

        }

        public void QuitGame()
        {
            Application.Quit();
            //Debug.Log("Quit");
        }

    }
}
