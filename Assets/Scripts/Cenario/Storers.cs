using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storers : MonoBehaviour
{
   public string ingredientCode;
   
   Ingredientes[] ingredientes;

   public float detectionDistance;

   void Start()
   {
       ingredientes = MacroSistema.sistema.Ingredientes;
   }
    
    public Ingredientes GiveIngredient()
    {
        int counter = 0; 

        foreach( Ingredientes ing in ingredientes)
        {
            if(ing.gameObject.activeInHierarchy)
            {
                counter ++;
            }
        }

        ingredientes[counter].gameObject.SetActive(true);
        ingredientes[counter].SetIngredient(ingredientCode);

        return ingredientes[counter];
    }

}
