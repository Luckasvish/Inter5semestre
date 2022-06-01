using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class Storers : _InteractionOBJ
{


    public override _Item itenOnThis { get; set; }
    public override bool hasItemOnIt {get; set;}
    public string ingredientName;

    internal override Material material{get ; set;}  
   void Awake()
   {   

       hasItemOnIt = true;

        
       
   }
    
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        material.SetFloat("_emission", 4);
    }
    public override _Item GiveItens(_Item Buffer)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {
        itenOnThis = MacroSistema.sistema.SpawnIngredient();
        itenOnThis.GetComponent<IngredientInstance>().SetMesh(ingredientName);
        itenOnThis.type = resetType();
        Debug.Log($"PATH#1: O ITEM SAIU DO STORER {itenOnThis.itemName}");
        Buffer = itenOnThis;
        itenOnThis = null;        
        return Buffer;
    }
    

    public override void ReceiveItens(_Item itens){}
     public override void Interact(_Item iten, PJ_Character chef)
     {
        if(iten == null) 
        {
            chef.ReceiveItens(this);
            Debug.Log($"PATH#2: O ITEM CHEGOU NA MAO DO CHEF {chef.itenInHand.itemName} , TIPO DO ITEM {chef.itenInHand.type}");
            RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick"); 
        }
          
        else Debug.Log("Já está com a mão cheia!");///////////////////////////////
     }


    ItemType resetType()
    {
        if(ingredientName == "Carne")   return ItemType._UnpreparedIngredient;
        
        else return ItemType._PreparedIngredient;
    }

}
