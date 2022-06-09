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
        Debug.Log(currentSceneIndex + " = currentSceneIndex");
        if (nextScene == "Menu")
        {
            currentSceneIndex = -1;
            Debug.Log(currentSceneIndex + " = Menu");
        }
        SceneManager.LoadScene(nextScene);
    }
    public static string SceneName()
    {
        string sceneName;
        switch (currentSceneIndex)
        {
            case 2:
                sceneName = "Dia 1";
                break;
            case 4:
                sceneName = "Dia 2";
                break;
            case 6:
                sceneName = "Dia 3";
                break;
            case 8:
                sceneName = "Dia 4";
                break;
            default:
                sceneName = "Dia";
                break;
        }
        return sceneName;
    }

    public static void PlaySFX()
    {
        RuntimeManager.PlayOneShot("event:/SFX UX/sfx_click");
    }

    public static void GoToNextScene()
    {
        Debug.Log(currentSceneIndex + " = currentSceneIndex");
        currentSceneIndex += 1;
        Debug.Log(currentSceneIndex + " = currentSceneIndex + 1");
        SceneManager.LoadScene(sceneOrder[currentSceneIndex]);
    }
}
