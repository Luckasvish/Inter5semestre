using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Oven : Interactable
{
    public override InteractableType type { get; set;}
    FeedBackManager feedback {get;set;}
    
    public override Item itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    public  GameObject highLight;
    public override bool highLightOn {get; set;}

    public Transform PanPosition;

    public Pan _Pan;

    void Awake()
    {
        type = InteractableType._Oven;
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
    
    void Start()
    {
        if(GetComponentInChildren<Pan>() == null)
        {
            _Pan = null;
            itenItHas = null;
        }
        else
        {
          hasItemOnIt=true;
          _Pan = GetComponentInChildren<Pan>();
          itenItHas = _Pan;
          itenItHas.transform.position = PanPosition.position;
        }
        
    }


    public override Item GiveItens(Item Buffer)
    {
       
            Buffer = itenItHas;
            itenItHas = null;
            hasItemOnIt = false;
            _Pan.onOven = false;
            _Pan = null;
            RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
            return Buffer;
       
    }

    public override void ReceiveItens(Item Pan)
    {
                _Pan = Pan.GetComponent<Pan>();
                itenItHas = _Pan;
                _Pan.onOven = true;
                itenItHas.transform.position = PanPosition.position;
                RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_put");
                hasItemOnIt = true;
       
    }

    public override void Interact(Item iten, Chef chef)
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
