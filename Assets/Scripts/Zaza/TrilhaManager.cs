using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;


public class TrilhaManager : MonoBehaviour
{

    public static TrilhaManager sound_Manager;
    internal EventInstance trilha;

    Scene   currentScene;

    void Awake()
    {
        trilha = RuntimeManager.CreateInstance("event:/MUSIC/music_gameplay");
        currentScene = SceneManager.GetActiveScene();


        if(sound_Manager == null)
        {
            sound_Manager = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    void Start()
    {

        if(currentScene.name == "Fase 1") { trilha.setParameterByName("menu", 0);}

        trilha.setParameterByName("menu", 1);
        FMOD.Studio.PLAYBACK_STATE sTATE;
        trilha.getPlaybackState(out sTATE);
        if(sTATE != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            trilha.start();
        }
    }


}
