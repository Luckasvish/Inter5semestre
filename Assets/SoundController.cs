using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;

public class SoundController : MonoBehaviour
{

    FMOD.Studio.VCA _VcaController;
    public string VcaName;
    private Slider slider;

    void Start()
    {  
         
        _VcaController = FMODUnity.RuntimeManager.GetVCA("vca:/" +VcaName);
        slider = GetComponent<Slider>();
    
    }

    public void SetVolume(float volume){    _VcaController.setVolume(volume);   }

}
