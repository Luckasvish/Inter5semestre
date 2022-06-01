using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJ_Character : MonoBehaviour
{
    ///COMPONENTES DE PLAYER///
    internal Movement_Manager movement_Manager;
    internal Input_Manager input_Manager;
    internal Detection_Manager detection_Manager;
    internal Animation_Manager anim_Manager;

    bool cutAnimSet;
    //////////////////////////


    ///CHEF
    public _Item itenInHand;    //  Referência pro item que player possuí. 
    public Transform itenPosition; // Cordenada da posição do item  *** REFATORAÇÃO: Setar os itens como children dos objetos pra não precisar reposicionar a todo frame.*** 
    ///////////////////////////


    // PERSONAGEM SELECIONADO 
    public bool characterOn;
    ///////////////////////////

    void Awake()    
    {
        input_Manager = MacroSistema.sistema.input_Manager;
        movement_Manager = GetComponent<Movement_Manager>();
        detection_Manager = GetComponent<Detection_Manager>();
        anim_Manager = GetComponentInChildren<Animation_Manager>();
        anim_Manager.main = this;
        movement_Manager.animation_Manager = anim_Manager;
    }


    void Update()
    {
        if(detection_Manager.canInteract == true && characterOn)
        {
            if(detection_Manager.preparerInteract == true)
            {
                PrepareInteraction(detection_Manager.GetPreparer());

            }



            else
            {
                if(input_Manager.pressedE == true)
                {
                    Interaction(detection_Manager.interactionOBJ);
                    anim_Manager.Take_Put();
                }
                
            }

        }
 
        if(CheckItem())
        {
            PositionItem(itenPosition.position);
            Debug.Log("ItemInHand: " + itenInHand.itemName);
        }


        if(MacroSistema.sistema.input_Manager.pressedSpace)
        {
            ToogleChar();
        }
        
        if(characterOn)
        {
        
            movement_Manager.Move(input_Manager.moveInput);
            
            
        }


    }
    
    // Método pra retornar se o player tem item ou não.
    public bool CheckItem(){if(this.itenInHand == null) return false;   else return true;}

    // Método para anular o item. (feito pra futura refatoração do posicionamento de item através da hierarquia).
    public void VoidItem() {itenInHand = null;}

    // Método pra posicionar o item. ***DELETAR DEPOIS DE REFATORAR O ITEM ***
    void PositionItem(Vector3 position){ itenInHand.transform.position = position;} 

    // Método para interagir. 
    public void Interaction(_InteractionOBJ interaction){ interaction.Interact(itenInHand, this);}

    public void PrepareInteraction(Preparer p)
    {
        if(p != null)
        {
            if(p.hasItemOnIt == true)
            {
                if(p.itenOnThis.type == ItemType._UnpreparedIngredient)
                {
                    if(input_Manager.holdE && input_Manager.moveInput.magnitude <= 0.3)
                    {
                        p.preparing = true;
                    
                        if(cutAnimSet == false)
                        {
                            anim_Manager.SetCut(true);
                            cutAnimSet = true;
                    
                        }
                    }

                    else 
                    {
                        p.preparing = false;
                        if(cutAnimSet == true)
                        {
                            anim_Manager.SetCut(false);
                            cutAnimSet = false;             
                        }
                    }
                }
                else 
                {   if(cutAnimSet == true)
                    {
                        anim_Manager.SetCut(false);
                        cutAnimSet = false;             
                    }

                    if(input_Manager.pressedE)  ReceiveItens(p);
                }
            }

            else 
            {
                if(input_Manager.pressedE) p.Interact(itenInHand,this);   
            }

        }


    }

    // Método para trocar de personagem.
    internal void ToogleChar(){ characterOn = !characterOn;}

    //Função de dar Item.
    internal _Item GiveIten(_Item Buffer)
    {   
        Buffer = itenInHand;
        VoidItem();
        return Buffer;
    }

    //Função de dar Ingradiente (Usada pela  panela). 
    internal IngredientInstance GiveIten(IngredientInstance Buffer)
    {   
        Buffer = itenInHand.GetComponent<IngredientInstance>();
        VoidItem();
        return Buffer;
    }

    // Função pra Receber itens.
    public void ReceiveItens(_InteractionOBJ interactedObj)//Função de receber itens, recebendo o item como parâmetro.
    {
        if(interactedObj != null)
        {
            itenInHand = interactedObj.GiveItens(interactedObj.itenOnThis);  
            itenInHand.transform.SetParent(this.gameObject.transform);
        }
        
    }
   

}
