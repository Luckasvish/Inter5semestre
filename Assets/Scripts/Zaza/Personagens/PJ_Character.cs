using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
public class PJ_Character : MonoBehaviour
{
    ///COMPONENTES DE PLAYER///
    internal Movement_Manager movement_Manager;
    internal Input_Manager input_Manager;
    internal Detection_Manager detection_Manager;
    internal Animation_Manager anim_Manager;
    bool cutAnimSet;
    //////////////////////////
    public static int playerOn; 

    public bool TutorialOff;


    ///CHEF
    public _Item itenInHand;    //  Referência pro item que player possuí. 
    public Transform itenPosition; // Cordenada da posição do item  *** REFATORAÇÃO: Setar os itens como children dos objetos pra não precisar reposicionar a todo frame.*** 

    ///////////////////////////


    // PERSONAGEM SELECIONADO 
    public bool characterOn;
    public Renderer[] renderers;    
    bool blinking;
    float blinkTimer;
    public float blinkTime;

    ///////////////////////////

    void Awake()    
    {
        renderers = GetComponentsInChildren<Renderer>();
        input_Manager = MacroSistema.sistema.input_Manager;
        movement_Manager = GetComponent<Movement_Manager>();
        detection_Manager = GetComponent<Detection_Manager>();
        anim_Manager = GetComponentInChildren<Animation_Manager>();
        anim_Manager.main = this;
        movement_Manager.animation_Manager = anim_Manager;
        playerOn = (characterOn)? 1: 0;
    }


    void Update()
    {
        anim_Manager.gameObject.transform.position = transform.position;
        if(detection_Manager.canInteract == true && characterOn)
        {
        
                if(input_Manager.pressedE == true)
                {
                    Interaction(detection_Manager.interactionOBJ);
                    anim_Manager.Take_Put();
                }
                
        }
        else anim_Manager.animator.SetBool("cutting",false);
 
        if(CheckItem())
        {
            PositionItem(itenPosition.position);
           // Debug.Log("TIPO DO ITEM:"+ itenInHand.itemName);
        }


        if(MacroSistema.sistema.input_Manager.pressedSpace)
        {
            ToogleChar();
        }
        
        if(characterOn)
        {
            movement_Manager.Move(input_Manager.moveInput);   
        }
   

        if(blinking == true)
        {
            Blink();
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

    


    void Blink()
    {
        blinkTimer += Time.deltaTime;

        foreach(Renderer r in renderers)
        {
            r.material.SetInt("_BlinkOn" ,1);
        }


        if(blinkTimer >= blinkTime)
        {
            foreach(Renderer r in renderers)
            {
                r.material.SetInt("_BlinkOn" ,0);
            }
            blinkTimer = 0;
            blinking = false;
        }



    }

    // Método para trocar de personagem.
    internal void ToogleChar()
    { 
        if(TutorialOff == true)
        {
            if(characterOn == false)
            {
                blinking = true;
                characterOn = true;
                RuntimeManager.PlayOneShot("event:/SFX UX/Sfx_hover");
                
            }
            else 
            {
                characterOn = false;
                anim_Manager.SetWalk(false);
            }
        }
    }
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
           
        }
        
    }
   

}
