using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedBackHUD : MonoBehaviour
{
    public Image barrinha;
   
    void Start()
    {
        RefreshBar();
    }

    internal void Gold()
    {
        barrinha.color = Color.green;
    }
    internal void SetBar(float fillAmount)
    {
        barrinha.fillAmount = fillAmount;
        if(fillAmount >= 1)
        {
            Gold();
        }
    }

    internal void RefreshBar()
    {
        barrinha.fillAmount = 0;
        barrinha.color = Color.red;
    }
}
