using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    VideoPlayer video;
    float videoTime;
    // Start is called before the first frame update
    void Start()
    {
        videoTime = (float)video.length;
        StartCoroutine(GoToNextScene());
    }

    IEnumerator GoToNextScene()
    {
        Debug.Log(videoTime + "videoTime");
        yield return new WaitForSeconds(videoTime);
        Debug.Log("finish");
        SceneManage.GoToNextScene();
    }
}
