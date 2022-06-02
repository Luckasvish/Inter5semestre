using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


public class TrilhaManager : MonoBehaviour
{

    public static TrilhaManager sound_Manager;
    EventInstance trilha;
    void Awake()
    {
        trilha = RuntimeManager.CreateInstance("event:/MUSIC/music_gameplay");


        if(sound_Manager == null)
        {
            sound_Manager = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    void Start()
    {
        trilha.setParameterByName("menu", 1);
        FMOD.Studio.PLAYBACK_STATE sTATE;
        trilha.getPlaybackState(out sTATE);
        if(sTATE != FMOD.Studio.PLAYBACK_STATE.PLAYING) trilha.start();
    }


}
