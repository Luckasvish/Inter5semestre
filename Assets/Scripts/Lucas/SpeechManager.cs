using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "speech", menuName = "ClientSpeech")]
public class SpeechManager : ScriptableObject
{
    [SerializeField]
    string[] clientSpeech;

    internal string RandomizeString()
    {
        int value = Random.RandomRange(0, clientSpeech.Length);
        return clientSpeech[value];
    }
}
