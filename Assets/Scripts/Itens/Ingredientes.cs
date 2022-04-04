using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredientes : Itens
{
    public string code;
    public float FcookingTime;
    public float AcookingTime;
    public float CcookingTime;
    internal float cookingTime;
    public bool prepared = true;


    public GameObject[] ingredients_Mesh;

    public void SetIngredient(string ingredientCode)
    {

        switch(ingredientCode)
        {
            case "F":

               ingredients_Mesh[0].SetActive(true);
               ingredients_Mesh[1].SetActive(false);
               ingredients_Mesh[2].SetActive(false);
               cookingTime = FcookingTime;    
               prepared = true;
                      
            break;
                
         
            case "A":

                ingredients_Mesh[0].SetActive(false);
                ingredients_Mesh[1].SetActive(true);
                ingredients_Mesh[2].SetActive(false); 
                cookingTime = AcookingTime;
                prepared = true;
                                
            break;


            case "C":

                ingredients_Mesh[0].SetActive(false);
                ingredients_Mesh[1].SetActive(false);
                ingredients_Mesh[2].SetActive(true); 
                cookingTime = CcookingTime;
                prepared = false;

            break;


            default : Debug.Log("Não identificou o código de Ingrediente ou código não é válido : " + code);
            break;
        }

        code = ingredientCode;



    }
    
}
