using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Itens
{
    string recipeCode;
    float cookingTimer;
    public int ingreOnPan = 0;

    bool cooking = false;
    public bool recipeReady = false ;

    Recipes[] recipes;

    void Awake()
    {
        cookingTimer = 0;
        recipes = MacroSistema.sistema.Recipes;
    }

    void Update()
    {
        if(cooking){Cooking();}
    }

    public void ReciveIngredient(Itens Ingredient)
    {
        if(ingreOnPan < 3)
        {
            Ingredientes ingre = Ingredient.GetComponent<Ingredientes>();

            if(ingre != null)
            {
                recipeCode += ingre.code;
                cookingTimer += ingre.cookingTime;
                ingreOnPan ++;
                ingre.gameObject.SetActive(false);
                cooking = true;
                
            }

        }
        else
        {
            Debug.Log("A panela está cheia!!");///
        }        
        
    }
    public void Cooking()
    {   
        Debug.Log(recipeCode);///
        Debug.Log(cookingTimer);///
        cookingTimer -=Time.deltaTime;
        if(cookingTimer <0)
        {
            cookingTimer = 0;
            Debug.Log("Comida Pronta");
            recipeReady = true;
        }

    }

    public Recipes GiveRecipe()
    {
        int counter = 0;

        foreach (Recipes plates in recipes)
        {
            if(plates.gameObject.activeInHierarchy)
            {
                counter ++;
            }
        }
        recipes[counter].gameObject.SetActive(true);
        recipes[counter].SetRecipe(recipeCode);
        recipeCode = "";
        cooking = false;
        cookingTimer = 0;
        return recipes[counter];
    }

}
