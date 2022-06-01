using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCutscene());
    }

    IEnumerator StartCutscene()
    {
        yield return new WaitForSeconds(4);
        SceneManage.GoToNextScene();
    }
}
