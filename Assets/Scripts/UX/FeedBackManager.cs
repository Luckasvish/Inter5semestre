using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackManager : MonoBehaviour
{
    public FeedBackHUD HUD;
    internal bool hudOn;
    internal float barMax;
    internal float currentBar;
    internal InteractableType type;


    void Awake()
    {
        Interactable interactable = GetComponent<Interactable>();
        if(interactable != null)
        {
            type = interactable.type;
        }
        else
        {
            type = InteractableType._Null;
        }
    }
    void Start()
    {
        if(HUD != null)
        {
            HUD.gameObject.SetActive(false);
        }
        hudOn = false;
    }
    internal void Run(float barSet)
    {
        HUD.SetBar(barSet);
    }
    internal void ToogleHUD()
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

