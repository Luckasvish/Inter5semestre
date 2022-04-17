using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storers : Interactable
{
    public override InteractableType type { get; set;}

    public override Item itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    public  GameObject highLight;
    public override bool highLightOn {get; set;}
    public int ingredientCode;

   void Awake()
   {   
       type = InteractableType._Storer;
       hasItemOnIt = true;
        
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
        itenItHas.GetComponent<IngredientInstance>().SetMesh(ingredientCode);
        Buffer = itenItHas;
        itenItHas = null;        
        return Buffer;
    }
    

    public override void ReceiveItens(Item itens){}

}
