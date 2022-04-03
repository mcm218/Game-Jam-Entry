using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
namespace Assets.Scripts
{
    public class StartButtonController : MonoBehaviour
    {
        // Not working
        public void GoToScene(int nextSceneBuildIndex)
        {
            Debug.Log("Going to " + nextSceneBuildIndex);
            try
            {
                Scene currentScene = SceneManager.GetActiveScene();

                if (nextSceneBuildIndex < 0) { Debug.LogError("Please set a valid build index"); return; }

                SceneManager.LoadScene(nextSceneBuildIndex);
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException)) { Debug.LogError("No scene has a build index of " + nextSceneBuildIndex); return; }
            }
        }
    }
}