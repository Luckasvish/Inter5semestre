using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public static class SceneManage
{
    public static string[] sceneOrder = { "Cutscene", "Loading", "Fase 1", "Loading", "Fase 2", "Loading", "Fase 3", "Loading", "Fase 4" };
    public static int currentSceneIndex = -1;
    public static void GoToScene(string nextScene)
    {
        currentSceneIndex = 0;
        SceneManager.LoadScene(nextScene);
    }

    public static void PlaySFX()
    {
        RuntimeManager.PlayOneShot("event:/SFX UX/sfx_click");
    }

    public static void GoToNextScene()
    {
        currentSceneIndex += 1;
        SceneManager.LoadScene(sceneOrder[currentSceneIndex]);
    }
}
