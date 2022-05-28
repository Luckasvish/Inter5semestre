using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
 public class Plates : _Item
 
 {

    public  override ItemType type { get; set; }
    public  override string itemName { get; set; }
   
    /////////////////////////////////////////////////////////////////////////
 

    //////////////////////////////////////////////////////////////////
    internal Recipe recipe;
    public Recipe[] recipeInstance;
    internal bool settle;
    
    //////////////////////////////////////////////////////////
    internal GameObject hud;
    
    public bool plateOn;

    void Awake()
    {
        Balcon balcon = GetComponentInParent<Balcon>();
        hud = GetComponentInChildren<PlateHUD>().gameObject;    
        hud.SetActive(false);
        recipe = new Recipe();    

        if(balcon != null)
        {
            balcon.itenItHas = GetComponent<Plates>();
            balcon.hasItemOnIt = true;
        }
        type = ItemType._Plate;
        
        if(plateOn)
        {
            settle =true;
            itemName = "Feijoada";

        }

    }
    
    public void ReceiveNewRecipe(string recipeName)
    { 
        switch(recipeName)
        {
            case "Feijoada":
                    recipe = recipeInstance[0];
                    
            break;
            case "PratoFeito":
                    recipe = recipeInstance[1];

            break;
            case "Buchada":
                    recipe = recipeInstance[2];
            break;
        }
        Debug.Log("Receita Escolhida: " + recipe.itemName);//
        recipe.InitiateRecipe();
    }

    public void ReceiveIngredient(string ingreName) 
    {
        bool added = false ;
       
       if(recipe.ingreNeeded.Count > 0)
       {
            for(int i = 0 ; i < recipe.ingreNeeded.Count ; i ++)
            {
                


                if(recipe.ingreNeeded[i].itemName == ingreName && added == false)
                {
                    
                    recipe.ReceiveIngredient(recipe.ingreNeeded[i]);
                    recipe.ingreNeeded.Remove(recipe.ingreNeeded[i]);
                    if(recipe.CheckRecipe())
                    {
                        itemName = recipe.itemName;
                        Debug.Log("Receita Pronta: " + itemName);
                    }
                    settle = true;
                    RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
                    added = true;
                }

                
                else 
                {
                    Debug.Log("Esse ingrediente não vai aqui!");////////////////////////
                }
            }
       }
       else 
       {
           Debug.Log("Esse prato está cheio!");////////////////////////
       }


    }

    public void CleanPlate()
    {
        foreach(GameObject obj in recipe.recipeIngre)
        {
            obj.SetActive(false);
        }
        itemName = "";
        settle = false;

    }

    public bool CheckIngredient(string ingreName)
    {
        for(int i = 0 ; i < recipe.ingreNeeded.Count ; i ++)
        {

            if(recipe.ingreNeeded[i].itemName == ingreName)
            {
                return true;
            }
        }
        
        return false;
    
    }
 }