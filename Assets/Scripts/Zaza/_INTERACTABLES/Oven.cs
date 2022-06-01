using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Oven : _InteractionOBJ
{
    // PROPRIEDADES DE INTERACTABLE (_INTERACTIONOBJ)
    public override _Item itenOnThis { get; set; }
    public override bool hasItemOnIt {get; set;}
    internal override Material material{get ; set;}  
    FeedBackManager feedback {get;set;} 
    /////////////////////////////////////////////////// 

    // PROPRIEDADES DE OVEN  
    internal Pan _Pan;




    void Start()
    {

        if(GetComponentInChildren<Pan>() == null)
        {
            _Pan = null;
            itenOnThis = null;
        }

        else
        {
          hasItemOnIt = true;
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
                itenOnThis.transform.SetParent(this.transform);
                RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_put");
                hasItemOnIt = true;
       
    }

    public override void Interact(_Item iten, PJ_Character chef)
    {

        if(iten != null)    //  PATH #1: (se o jogador tiver um item na mão)
        { 
           ItemType type = iten.type;
            switch(type)
            {

                case ItemType._PreparedIngredient:  // PATH #1.1 (se o item do jogador for um ingrediente preparado)

                    IngredientInstance ingre = iten.GetComponent<IngredientInstance>();
                    
                    if(hasItemOnIt == true && _Pan.ingredient == null ) _Pan.ReceiveItens(chef.GiveIten(ingre));    //  (se o fogão tiver uma panela e a panela estiver vazia ) {a panela recebe o ingrediente}
                    
                    else if(hasItemOnIt == true && _Pan.ingredient != null) Debug.Log("Já tem um ingrediente nessa panela!!"); //////// 
                   
                    else Debug.Log("Está faltando a panela !!!"); ////////////////////////////////////  

                break;
                
                case ItemType._Plate:   //  PATH # 1.2 (se o item do jogador for um prato)
                    
                    Plates plate =  iten.GetComponent<Plates>();
                    
                    if(hasItemOnIt == true) //  (se o fogão tiver uma panela)
                    {
                        if(_Pan.finishedCooking == true && plate.recipe != null)    //   (se a panela já tiver terminado de cozinhar) { o prato tenta receber o ingrediente }   
                        {
                            if(plate.CheckIngredient(_Pan.ingredient.itemName) == true) plate.ReceiveIngredient(_Pan.GiveItem(_Pan.ingredient.itemName));  
                        }
                        
                        else Debug.Log("Esse ingrediente ainda não está pronto"); ////////////////////////////////////////
                    }
                    
                    else Debug.Log("Está faltando a panela !!!"); ///////////////////////////////////////
                
                break;

                case ItemType._Pan: //  PATH #1.3 (se o item do jogador for uma panela)

                    
                    if(hasItemOnIt == false) ReceiveItens(chef.GiveIten(chef.itenInHand));  //  (se não tiver panela )  { o fogão recebe a panela}
                    
                    else Debug.Log("Já tem panela aqui"); /////////////////////////////// 
                    
                    
                break;

                default : Debug.Log("Deu ruim!!"); break;

            }
        }
        else // PATH #2 (se o jogador não tiver nenhum item)
        {
            
            if(hasItemOnIt) chef.ReceiveItens(this); //  (se o fogão tiver uma panela)   {o jogador recebe a panela}

            else Debug.Log("Não tem nada aqui!");////////////////////
        
        }
    }
    
}
