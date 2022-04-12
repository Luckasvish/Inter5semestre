using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : Itens
{
    public override string itemName { get; set;}
    public override ItenType type { get; set;}

    public GameObject[] ingredients_Mesh;


    void Awake()
    {
        type = ItenType._UnpreparedIngredient;
    }
    public void SetIngredient(string ingredientCode)
    {

        switch(ingredientCode)
        {
            case "F":

               ingredients_Mesh[0].SetActive(true);
               ingredients_Mesh[1].SetActive(false);
               ingredients_Mesh[2].SetActive(false);
               type = ItenType._PreparedIngredient;    
                     
            break;
                
         
            case "A":

                ingredients_Mesh[0].SetActive(false);
                ingredients_Mesh[1].SetActive(true);
                ingredients_Mesh[2].SetActive(false); 
                type = ItenType._PreparedIngredient;
                                
            break;


            case "C":

                ingredients_Mesh[0].SetActive(false);
                ingredients_Mesh[1].SetActive(false);
                ingredients_Mesh[2].SetActive(true); 
                type = ItenType._UnpreparedIngredient;

            break;


            default :Debug.Log("INGREDIENT: Código Inválido");///
            break;
        }
        itemName = ingredientCode;
    }
    
}
