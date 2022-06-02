using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField]
    VideoPlayer video;
    float videoTime;
    [SerializeField]
    string scene;
    // Start is called before the first frame update
    void Start()
    {
        videoTime = (float)video.length;
        StartCoroutine(GoToNextScene());
    }

    IEnumerator GoToNextScene()
    {
        yield return new WaitForSeconds(videoTime);
        SceneManage.GoToScene(scene);
    }
}
