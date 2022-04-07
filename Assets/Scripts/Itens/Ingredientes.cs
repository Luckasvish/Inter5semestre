using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredientes : Itens
{
    public override string itemName { get; set;}
    public override ItenType type { get; set;}
    public float FcookingTime;
    public float AcookingTime;
    public float CcookingTime;
    internal float cookingTime;
    public bool prepared = true;

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
               cookingTime = FcookingTime;
               type = ItenType._PreparedIngredient;    
               prepared = true;
                      
            break;
                
         
            case "A":

                ingredients_Mesh[0].SetActive(false);
                ingredients_Mesh[1].SetActive(true);
                ingredients_Mesh[2].SetActive(false); 
                cookingTime = AcookingTime;
                type = ItenType._PreparedIngredient;
                prepared = true;
                                
            break;


            case "C":

                ingredients_Mesh[0].SetActive(false);
                ingredients_Mesh[1].SetActive(false);
                ingredients_Mesh[2].SetActive(true); 
                cookingTime = CcookingTime;
                type = ItenType._UnpreparedIngredient;
                prepared = false;

            break;


            default :
            break;
        }
        itemName = ingredientCode;
    }
    
}
