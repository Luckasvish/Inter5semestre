using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
 public class Plates : Item
 
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
                    itemName = recipe.itemName;
                    
            break;
            case "PratoFeito":
                    recipe = recipeInstance[1];
                    itemName = recipe.itemName;

            break;
            case "Buchada":
                    recipe = recipeInstance[2];
                    itemName = recipe.itemName;
            break;
        }

        Debug.Log("Nome da receita no prato: " + itemName);///////////////
        recipe.InitiateRecipe();
    }

    public void ReceiveIngredient(string ingreName) 
    {
        bool added = false ;
       
        for(int i = 0 ; i < recipe.ingreNeeded.Count ; i ++)
        {
            if(recipe.ingreNeeded[i].itemName == ingreName && added == false)
            {
                recipe.ReceiveIngredient(recipe.ingreNeeded[i]);
                recipe.ingreNeeded.Remove(recipe.ingreNeeded[i]);
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

    public bool CheckIngredient(string ingreName)
    {
        for(int i = 0 ; i < recipe.ingreNeeded.Count ; i ++)
        {
            Debug.Log(recipe.ingreNeeded[i].itemName);
            if(recipe.ingreNeeded[i].itemName == ingreName)
            {
                return true;
            }
        }
        
        return false;
    
    }
 }