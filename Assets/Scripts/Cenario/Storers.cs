using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storers : Interactable
{
    public override InteractableType type { get; set;}

    public override Item itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    public override bool highLightOn {get; set;}
    public string ingredientName;

    public override Material OriginalMaterial {get; set;}
    
    public override Material FlashMaterial{get ; set;}
    
    public override MeshRenderer mesh{get; set;}

   void Awake()
   {   
       type = InteractableType._Storer;
       hasItemOnIt = true;
        mesh = this.GetComponent<MeshRenderer>();
        OriginalMaterial = mesh.material;
         FlashMaterial = MacroSistema.sistema.flashMaterial;
        
       
   }
    
    void Update()
    {
   
    }
     public override void ToogleHighLight(bool On)
    {

        if(On)
        {
            mesh.material = FlashMaterial;
        }
        else
        {
             mesh.material = OriginalMaterial;
        }

    }
    public override Item GiveItens(Item Buffer)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {
        itenItHas = MacroSistema.sistema.SpawnIngredient();
        itenItHas.GetComponent<IngredientInstance>().SetMesh(ingredientName);
        Buffer = itenItHas;
        itenItHas = null;        
        return Buffer;
    }
    

    public override void ReceiveItens(Item itens){}
     public override void Interact(Item iten, Chef chef)
     {
        if(iten == null) chef.ReceiveItens(this);
          else Debug.Log("Já está com a mão cheia!");///////////////////////////////
     }

}
