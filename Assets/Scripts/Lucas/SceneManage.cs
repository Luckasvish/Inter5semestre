using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManage
{
    public static string[] sceneOrder = { "Cutscene", "Loading", "Fase1", "Loading", "Fase2", "Loading", "Fase3", "Loading", "Fase4" };
    public static int currentSceneIndex = -1;
    public static void GoToScene(string nextScene)
    {
        currentSceneIndex = 0;
        SceneManager.LoadScene(nextScene);
    }

    public static void GoToNextScene()
    {
        currentSceneIndex += 1;
        SceneManager.LoadScene(sceneOrder[currentSceneIndex]);
    }
}
