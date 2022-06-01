using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackManager : MonoBehaviour
{
    public UI   HUD;
    internal bool hudOn;
    internal float barMax;
    internal float currentBar;
    internal InteractableType type;


    void Awake()
    {
        _InteractionOBJ interactable = GetComponent<_InteractionOBJ>();
    }
    void Start()
    {
        if(HUD != null)
        {
            HUD.gameObject.SetActive(false);
        }
        hudOn = false;
    }
    internal void RunSlider(float barSet)
    {
        HUD.SetBar(barSet);
    }
    internal void ToogleUI()
    {
        if(hudOn)
        {
            HUD.RefreshBar();
            HUD.gameObject.SetActive(false);
            hudOn = false;
        }
        else
        {
            HUD.gameObject.SetActive(true);
            hudOn = true;
        }
    }
}

