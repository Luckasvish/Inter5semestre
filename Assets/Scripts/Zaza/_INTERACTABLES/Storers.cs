using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class Storers : _InteractionOBJ
{


    public override _Item itenOnThis { get; set; }
    public override bool hasItemOnIt {get; set;}
    public string ingredientName;
    internal override bool blinking{get ; set;}  
    internal override float blinkTimer {get; set;}


    public Renderer renderers;    
   

    public float blinkTime;
   void Awake()
   {   

       hasItemOnIt = true;

        
   }
   void Update()
   {
       if(blinking == true) Blink();
   }
    public void Blink()
    {
        blinkTimer += Time.deltaTime;
        renderers.material.SetInt("_BlinkOn" , 1);

        if(blinkTimer >= blinkTime)
        {
            StopBlinking();
        }

    }

    public void StopBlinking()
    {
        renderers.material.SetInt("_BlinkOn" , 0);
        blinking = false;
        blinkTimer = 0;
    }

    
    public override _Item GiveItens(_Item Buffer)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {
        itenOnThis = MacroSistema.sistema.SpawnIngredient();
        itenOnThis.GetComponent<IngredientInstance>().SetMesh(ingredientName);
        itenOnThis.type = resetType();
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
