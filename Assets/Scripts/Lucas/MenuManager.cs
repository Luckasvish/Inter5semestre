using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void Play() 
    { 
        SceneManage.GoToNextScene();
    }

    public void GoToScene(string _nextScene)
    {
        SceneManage.GoToScene(_nextScene);
    }

}
