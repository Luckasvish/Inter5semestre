using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storers : Interagibles
{
    public override InteragibleType type { get; set;}
    public override Itens itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
   Ingredientes[] ingredientes;
   public string ingredientCode;
   int counter; 


   void Awake()
   {   
       type = InteragibleType._Storer;
       ingredientes = MacroSistema.sistema.Ingredientes;
    
       hasItemOnIt = true;
   }

    
    public override Itens GiveItens(Itens itenToGive)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {
        ingredientes[counter].gameObject.SetActive(true);
        ingredientes[counter].SetIngredient(ingredientCode);
        itenItHas = ingredientes[counter];
        itenToGive = itenItHas;
        itenItHas = null;
        return itenToGive;
    }

    public void CountItenToSpawn()
    {
        counter = 0;
        foreach( Ingredientes ing in ingredientes)
        {
            if(ing.gameObject.activeInHierarchy)
            {
                counter ++;
            }
        }
    }
    public override void ReceiveItens(Itens itens){}

}
