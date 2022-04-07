using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preparer : Interagibles
{
    
    public override InteragibleType type { get; set;}
    public override Itens itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    public Transform ingredientPosition;


    public float preparationTime;
    float preparationTimer;

    internal bool prepared;
    void Awake()
    {
        type = InteragibleType._Preparer;
        itenItHas = null;
    }

    void Update()
    {
        if(hasItemOnIt)
        {
            PrepareIngredient();
        }

        Debug.Log(preparationTimer);///
        Debug.Log(prepared);///

    }
    public override void ReceiveItens(Itens itenInHand)
    {  
            itenItHas = itenInHand;

            itenItHas.transform.position = ingredientPosition.position;

            preparationTimer = preparationTime;

            hasItemOnIt = true;
            
            prepared = false;
    }


    public void PrepareIngredient()
    {
            preparationTimer -= Time.deltaTime;
            if(preparationTimer <= 0)
            {
                prepared = true;
                itenItHas.GetComponent<Ingredientes>().type = ItenType._PreparedIngredient;
            }
    }




     public override Itens GiveItens(Itens itenToGive)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {   if(itenItHas.type == ItenType._PreparedIngredient)
        {   
            itenToGive = itenItHas;
            itenItHas = null;
            hasItemOnIt = false;
            return itenToGive;
        }
        else 
        {
            Debug.Log("Não foi preparado ainda !");///
            return null;
        }

    }
    
   
}
