using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "speech", menuName = "ClientSpeech")]
public class SpeechManager : ScriptableObject
{
    public static int storyIndex;
    [SerializeField]
    string[] clientSpeech;

    internal string ChooseLoadingStory()
    {
        string story_ = clientSpeech[storyIndex];
        if(storyIndex < 3)
        storyIndex++;
        else
            storyIndex = 0;
        return story_;
    }
    internal string RandomizeString()
    {
        int value = Random.RandomRange(0, clientSpeech.Length);
        return clientSpeech[value];
    }
}
