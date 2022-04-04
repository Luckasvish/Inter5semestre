using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preparer : MonoBehaviour
{
    
    public Transform ingredientPosition;
    internal Ingredientes ingrediente;
    internal bool hasIngredientOnIt;


    public void ReceiveIngredients(Itens itenInHand)
    {
        
        itenInHand.transform.position = ingredientPosition.position;
        ingrediente = itenInHand.GetComponent<Ingredientes>();
        hasIngredientOnIt = true;
        if(ingrediente.prepared == false)
        {
            ingrediente.prepared = true;
        }
        else
        {
            Debug.Log("Esse ingrediente já está preparado!!!");
        }
    }


    public Itens GivePreparedIngredient(Itens Buffer)
    {
        Buffer = ingrediente;
        hasIngredientOnIt = false;
        ingrediente = null;
        return Buffer;

    }
    
   
}
