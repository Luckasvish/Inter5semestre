using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
 public class Plates : _Item
 
 {
     // PROPRIEDADES DE ITEM
    public  override ItemType type { get; set; }
    public  override string itemName { get; set; }
   
    /////////////////////////////////////////////////////////////////////////
    
    // PROPRIEDADES DE PLATE
    internal Recipe recipe;
    public Recipe[] recipeInstance;
    internal bool settle;
    //////////////////////////////////////////////////////////////////

    // UI / UX
    internal GameObject hud;
    //////////////////////////////////////////////////////////
    
    public bool plateOn;    //  bool pra deixar o prato preparado DEBUG

    void Awake()
    {
        type = ItemType._Plate;

        Balcon balcon = GetComponentInParent<Balcon>();
        hud = GetComponentInChildren<RecipeChoise>().gameObject;    
        hud.SetActive(false);
        recipe = recipeInstance[0];  
        recipe.InitiateRecipe();  

    
        if(balcon != null)
        {
            balcon.itenOnThis = GetComponent<Plates>();
            balcon.hasItemOnIt = true;
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
        recipe.InitiateRecipe();
    }

    public void ReceiveIngredient(string ingreName) 
    {
        bool added = false ;
        Debug.Log("IngreReceived: " + ingreName);
       
       if(this.recipe.ingreNeeded.Count > 0)
       {
            foreach(Ingredient i in this.recipe.ingreNeeded)
            {
                    
                
                if(i.itemName == ingreName && added == false)
                {
                    this.recipe.ReceiveIngredient(i);
                    this.recipe.ingreNeeded.Remove(i);
                    settle = true;
                 
                    if(recipe.CheckRecipe())
                    {
                        itemName = recipe.itemName;
                        Debug.Log("Receita Pronta: " + itemName);
                    }
                 
                 
                    RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
                 
                    added = true;
                    Debug.Log("IngreAdeed:" + i.itemName);///////
                    Debug.Log("IngresNeeded:" + recipe.ingreNeeded.Count);///////
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
        foreach(GameObject obj in recipe.recipeIngre){   obj.SetActive(false);}
        recipe.InitiateRecipe();
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