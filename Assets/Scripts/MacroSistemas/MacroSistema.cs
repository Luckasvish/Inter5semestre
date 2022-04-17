using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacroSistema : MonoBehaviour
{
    public static MacroSistema sistema;
    public IngredientInstance[] IngredientsInstance = new IngredientInstance[5];

    public static string[] staticRecipes;

    int[] counter = new int[5];

   [SerializeField] internal Input_Manager input_Manager; ///Sistema de Inputs.
  


    void Awake()
    {
        if (sistema == null)
        {
            sistema = this;
        }

        staticRecipes = new string[3];

        staticRecipes[0] = "Feijoada";
         staticRecipes[1] = "PratoFeito";
          staticRecipes[2] = "Buchada";


        foreach(Item i in IngredientsInstance)
        {
            i.gameObject.SetActive(false);
        }
      
    }
    
    public string GetPossibleRecipes( int recipeIndex)
    {
        return staticRecipes[recipeIndex];
    }
    
    
        void Start()
    {
         for (int i = 0; i < IngredientsInstance.Length; i++)
        { counter[i]= 0;}
    }


    public Item SpawnIngredient(int code)
    {
       bool spawned = false;
        int index = 0;
       
        for( int i = 0; i < counter.Length; i++)
        {
            if(counter[i] == 0 && spawned == false)
            {
                counter[i] = 1;
                spawned = true;
                index = i;
            }
        }

        if(index >= 5)
        {
            Debug.Log("Já atingiu o limite de itens!!!");
            return null;
        }
        else 
        {
            IngredientsInstance[index].gameObject.SetActive(true);
            Debug.Log("Spawnou");
            return IngredientsInstance[index];
        }
    }

}
