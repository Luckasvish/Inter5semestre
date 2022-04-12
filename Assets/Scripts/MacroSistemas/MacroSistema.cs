using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacroSistema : MonoBehaviour
{
   public static MacroSistema sistema;
   public Ingredients[] Ingredients = new Ingredients[5];
   public Recipes[] Recipes = new Recipes[5];

   [SerializeField] internal Input_Manager input_Manager; ///Sistema de Inputs.
  
    void Awake()
    {
        if (sistema == null)
        {
            sistema = this;
        }

        foreach(Itens i in Ingredients)
        {
            i.gameObject.SetActive(false);
        }
      
         foreach(Itens i in Recipes)
        {
            i.gameObject.SetActive(false);
        }      
    }


    public Itens SpawnIngredient(Storers interaction)
    {
        int counter = 0;

        foreach( Ingredients ing in Ingredients)
        {
            if(ing.gameObject.activeInHierarchy)
            {
                counter ++;
            }
        }
        Ingredients[counter].gameObject.SetActive(true);
        Ingredients[counter].SetIngredient(interaction.ingredientCode);
        return Ingredients[counter];

    }

    public Itens SpawnRecipe(Plates plate)
    {
        int counter = 0;

        foreach( Recipes rec in Recipes)
        {
            if(rec.gameObject.activeInHierarchy)
            {
                counter ++;
            }
        }
        Recipes[counter].gameObject.SetActive(true);
        Recipes[counter].transform.position = plate.pl_FoodPosition.position;
        return Recipes[counter];
    }


}
