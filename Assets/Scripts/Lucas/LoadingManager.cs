using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    SpeechManager storyLoading;    
    [SerializeField]
    TextMeshProUGUI storyText;
    void Start()
    {
        StartCoroutine(StartLoading());
        storyText.text = storyLoading.ChooseLoadingStory();
    }

    IEnumerator StartLoading()
    {
        yield return new WaitForSeconds(6);
        SceneManage.GoToNextScene();
    }
}
