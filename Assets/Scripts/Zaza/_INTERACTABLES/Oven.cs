using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Oven : _InteractionOBJ
{
    // PROPRIEDADES DE INTERACTABLE (_INTERACTIONOBJ)
    public override InteractableType type { get; set;}

    public override _Item itenOnThis { get; set; }
    public override bool hasItemOnIt {get; set;}
    internal override Material material{get ; set;}  
    FeedBackManager feedback {get;set;} 
    /////////////////////////////////////////////////// 

    // PROPRIEDADES DE OVEN  
    internal Pan _Pan;

    void Awake(){   type = InteractableType._Oven;  }


    void Start()
    {

        if(GetComponentInChildren<Pan>() == null)
        {
            _Pan = null;
            itenOnThis = null;
        }
        else
        {
          hasItemOnIt=true;
          _Pan = GetComponentInChildren<Pan>();
          itenOnThis = _Pan;
        }

        material = GetComponent<MeshRenderer>().material;
        material.SetFloat("_emission", 4);
        
    }




    public override _Item GiveItens(_Item Buffer)
    {
       
            Buffer = itenOnThis;
            itenOnThis = null;
            hasItemOnIt = false;
            _Pan.onOven = false;
            _Pan = null;
            RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
            return Buffer;
       
    }

    public override void ReceiveItens(_Item Pan)
    {
                _Pan = Pan.GetComponent<Pan>();
                itenOnThis = _Pan;
                _Pan.onOven = true;
                _Pan.Position();    // Posiciona a panela ao recebe-la.
                RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_put");
                hasItemOnIt = true;
       
    }

    public override void Interact(_Item iten, PJ_Character chef)
    {

        if(iten != null)
        { 
           ItemType type = iten.type;
            switch(type)
            {

                case ItemType._PreparedIngredient:

                    IngredientInstance ingre = iten.GetComponent<IngredientInstance>();
                    if(hasItemOnIt == true) _Pan.ReceiveItens(chef.GiveIten(ingre));
                      else  Debug.Log("Está faltando a panela !!!"); ////////////////////////////////////  

                break;
                
                case ItemType._Plate:
                    
                    Plates plate =  iten.GetComponent<Plates>();
                    
                    if(hasItemOnIt == true)
                    {
                        if(_Pan.ready.activeInHierarchy && plate.recipe != null) plate.ReceiveIngredient(_Pan.GiveItem(_Pan.ingredient.itemName));   
                          else Debug.Log("Ainda não está pronto"); ////////////////////////////////////////
                    }
                     else Debug.Log("Está faltando a panela !!!"); ///////////////////////////////////////
                
                break;

                case ItemType._Pan:

                    if(hasItemOnIt == false) ReceiveItens(chef.GiveIten(chef.itenInHand));
                      else Debug.Log("Já tem panela aqui"); /////////////////////////////////////////////////////////////////////////////////////////// 
                    
                    
                break;

                default : Debug.Log("Deu ruim!!"); break;

            }
        }
        else 
        {
           if(hasItemOnIt) chef.ReceiveItens(this);
            else Debug.Log("Não tem nada aqui!");////////////////////
        }
    }
    
}
