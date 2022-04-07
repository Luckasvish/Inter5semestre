using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipes : Itens

{
   public  override ItenType type { get; set; }
   public  override string itemName { get; set; }
   
   public int price;

   public GameObject[] recipes_Mesh;
   void Awake()
   {
         type = ItenType._Recipe;
   }
   public void SetRecipe(string recipeCode)
   {  
   
      switch(recipeCode)
      {
            case "FFC" :
            case "FCF" :
            case "CFF" :

                  recipes_Mesh[0].SetActive(true);    
                  recipes_Mesh[1].SetActive(false);
                  recipes_Mesh[2].SetActive(false);
                  itemName = "Feijoada";
            break;
                
         
            case "AFC" :
            case "ACF" :
            case "CFA" :
            case "CAF" :
            case "FAC" :
            case "FCA" :

                  recipes_Mesh[0].SetActive(false);    
                  recipes_Mesh[1].SetActive(true);
                  recipes_Mesh[2].SetActive(false);  
                  itemName = "PratoFeito";
                                
            break;


            case "FaCC" :
            case "CFaC" :
            case "CCFa" :

                  recipes_Mesh[0].SetActive(false);    
                  recipes_Mesh[1].SetActive(false);
                  recipes_Mesh[2].SetActive(true); 
                  itemName = "Buchada"; 

            break;


            default:
            break;
      }

   }

}
