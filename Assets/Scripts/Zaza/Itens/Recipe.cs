using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
   public string itemName ;
   public GameObject[] recipeIngre; 

   public int price;
   internal List<Ingredient> ingreNeeded = new List<Ingredient>();   


   public void InitiateRecipe()
   {
         ingreNeeded.Clear();

         foreach(GameObject obj in recipeIngre)
         {
               if(obj.GetComponent<Ingredient>() != null)
               {
                  ingreNeeded.Add(obj.GetComponent<Ingredient>());
               }
         }
   }   

      public void ReceiveIngredient(Ingredient ingredient)
      {
            ingredient.gameObject.SetActive(true);    
            CheckRecipe();
      }


      internal bool CheckRecipe()
      {

            int counter = 0;

            foreach(GameObject obj in recipeIngre)
            {
                  if(obj.activeInHierarchy)
                  {
                        counter ++;
                  }
            }

            if (counter == recipeIngre.Length)
            {
                  return true;
            }
            else return false;


      }
}

