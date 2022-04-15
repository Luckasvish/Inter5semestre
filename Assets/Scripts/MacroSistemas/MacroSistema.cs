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

        foreach(Item i in Ingredients)
        {
            i.gameObject.SetActive(false);
        }
      
         foreach(Item i in Recipes)
        {
            i.gameObject.SetActive(false);
        }      
    }


    public Item SpawnIngredient(string code)
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
        switch(code)
        {
            case "Beans":    Ingredients[counter].SpawnBeans();
            break;
            case "Rice":    Ingredients[counter].SpawnRice();
            break;   
            case "Meat":    Ingredients[counter].SpawnMeat();
            break;
            default: Debug.Log("Deu ruim no código de ingrediente !: " + code);
            break;
        }
        
        return Ingredients[counter];

    }

    public Item SpawnRecipe(Plates plate)
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
