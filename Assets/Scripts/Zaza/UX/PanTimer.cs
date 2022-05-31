using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;


public class PanTimer : MonoBehaviour
{  
    Pan main;
    public GameObject HUD;
    public  Image timer;

    internal EventInstance cookingSfxEvent;
    internal EventInstance burningSfxEvent;
    public GameObject[] sprites;

    internal Color green;
    internal Color red;
    void Start()
    {
        main = GetComponentInParent<Pan>();
        main.Timer = this;
        HUD.gameObject.SetActive(false);
        cookingSfxEvent = RuntimeManager.CreateInstance("event:/SFX GAMEPLAY/sfx_cooking");
        burningSfxEvent = RuntimeManager.CreateInstance("event:/SFX GAMEPLAY/sfx_burning");
        green = timer.color;
        red = new Color32(200,48,20,255);
        timer.color = red;

    }


    void Update()
    {
        if(main.onOven == true && main.ingredient != null)
        {
            if(main.ingredient.type == ItemType._PreparedIngredient) SetTimer(main.currentTime, main._cockingTime);

            else if(main.ingredient.type == ItemType._CookedIngredient) SetTimer(main.currentTime, main._burnTime);
    
        }
        
    }

    //Método para ligar e desligar o Timer.
    public void ToogleTimer()
    {
        if(HUD.activeInHierarchy)
        { 
            HUD.SetActive(false);
        }
        else  
        {
            CheckImageToSet();
            HUD.SetActive(true);   
        }
    }

    //Método para checar qual imagem ativar na HUD.
    public void CheckImageToSet()
    {
        int i = 0;

        switch(main.ingredient.itemName)
        {
            case "Feijao":      if(main.ingredient.type == ItemType._PreparedIngredient)    i = 2;
                                else if (main.ingredient.type == ItemType._CookedIngredient)    i=  3;
            break;
            
            case "Arroz":       if(main.ingredient.type == ItemType._PreparedIngredient)    i = 0;
                                else if (main.ingredient.type == ItemType._CookedIngredient)    i=  1;
            
            break;
            
            case "Carne":       if(main.ingredient.type == ItemType._PreparedIngredient)    i = 4;
                                else if (main.ingredient.type == ItemType._CookedIngredient)    i=  5;
            
            break;
            
            case "Farofa":     if(main.ingredient.type == ItemType._PreparedIngredient)    i = 6;
                                else if (main.ingredient.type == ItemType._CookedIngredient)    i=  7;
            
            break;

        }
        foreach(GameObject obj in sprites){ obj.SetActive(false);}

        if(main.ingredient.type != ItemType._BurnedIngrediente)
        {
            sprites[i].SetActive(true);
        }
        else
        {
            sprites[8].SetActive(true);
        }
                               
    }

    //Função para rodar o Timer.
    void SetTimer(float x , float y)
    {   
       
        timer.fillAmount = Mathf.Lerp(0, 1, x/y);
    }



}
