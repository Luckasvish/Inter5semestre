using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartLoading());
    }

    IEnumerator StartLoading()
    {
        yield return new WaitForSeconds(4);
        SceneManage.GoToNextScene();
    }
}
