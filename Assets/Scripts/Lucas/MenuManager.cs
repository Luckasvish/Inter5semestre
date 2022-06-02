using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject InstructionsUI;

    public void Play() 
    { 
        SceneManage.GoToNextScene();
    }

    public void GoToScene(string _nextScene)
    {
        SceneManage.GoToScene(_nextScene);
    }    
    
    public void Hide(bool active)
    {
        InstructionsUI.SetActive(active);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
