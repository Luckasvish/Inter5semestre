using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storers : _InteractionOBJ
{
    public override InteractableType type { get; set;}

    public override _Item itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    public string ingredientName;

    internal override Material material{get ; set;}  
   void Awake()
   {   
       type = InteractableType._Storer;
       hasItemOnIt = true;

        
       
   }
    
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        material.SetFloat("_emission", 4);
    }
    public override _Item GiveItens(_Item Buffer)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {
        itenItHas = MacroSistema.sistema.SpawnIngredient();
        itenItHas.GetComponent<IngredientInstance>().SetMesh(ingredientName);
        Buffer = itenItHas;
        itenItHas = null;        
        return Buffer;
    }
    

    public override void ReceiveItens(_Item itens){}
     public override void Interact(_Item iten, Chef chef)
     {
        if(iten == null) chef.ReceiveItens(this);
          else Debug.Log("Já está com a mão cheia!");///////////////////////////////
     }

}
