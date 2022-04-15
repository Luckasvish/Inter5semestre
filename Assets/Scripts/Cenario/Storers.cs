using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storers : Interactable
{
    public override InteractableType type { get; set;}

    public override Item itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    public override GameObject highLight { get ; set ; }
    public override bool highLightOn {get; set;}
    public string ingredientCode;

   void Awake()
   {   
       type = InteractableType._Storer;
       hasItemOnIt = true;
        highLight = GetComponentInChildren<Light>().gameObject;
        highLight.SetActive(false);
   }
    
    void Update()
    {
      if(highLightOn)
      {
        highLight.SetActive(true);
      }
      else 
      {
        highLight.SetActive(false);
      }
    }
    public override Item GiveItens(Item Buffer)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {
        itenItHas = MacroSistema.sistema.SpawnIngredient(ingredientCode);
        Buffer = itenItHas;
        itenItHas = null;        
        return Buffer;
    }
    

    public override void ReceiveItens(Item itens){}

}
