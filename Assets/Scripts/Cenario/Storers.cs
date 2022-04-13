using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storers : Interactable
{
    public override InteractableType type { get; set;}
    public override FeedBackManager feedback {get;set;}
    public override Itens itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    public override GameObject highLight { get ; set ; }
    public string ingredientCode;

   void Awake()
   {   
       type = InteractableType._Storer;
       hasItemOnIt = true;
   }

    public override void TurnHighLightOn()
    {
      highLight.SetActive(true);
    }
    
    public override Itens GiveItens(Itens Buffer)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {
        itenItHas = MacroSistema.sistema.SpawnIngredient(this);
        Buffer = itenItHas;
        itenItHas = null;
        return Buffer;
    }
    

    public override void ReceiveItens(Itens itens){}

}
