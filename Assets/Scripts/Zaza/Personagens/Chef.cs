using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    ///COMPONENTES DE PLAYER///
    internal Movement_Manager movement_Manager;
    internal Input_Manager input_Manager;
    internal Detection_Manager detection_Manager;
    ///--------------------///


    ///CHEF ---
    public _Item itenInHand;
    public bool hasItem;
    public Transform itenPosition;

    public bool characterOn;

    void Awake()
    {
        input_Manager = MacroSistema.sistema.input_Manager;
        movement_Manager = GetComponent<Movement_Manager>();
        detection_Manager = GetComponent<Detection_Manager>();
    }


    void Update()
    {
        if(detection_Manager.canInteract == true && characterOn)
        {
            if (input_Manager.pressedE == true)
            {
                Interaction(detection_Manager.interactionOBJ );
            }
        }
 
        if(hasItem == true)
        {
            itenInHand.transform.position = itenPosition.position;
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

    public void Interaction(_InteractionOBJ interaction)
    {
        interaction.Interact(itenInHand, this);
    }

    internal void ToogleChar()
    {
        characterOn = !characterOn;
    }

    internal _Item GiveIten(_Item Buffer)//Função de dar Item.
    {   
        Buffer = itenInHand;
        itenInHand = null; 
        hasItem = false;
        return Buffer;
    }

    internal IngredientInstance GiveIten(IngredientInstance Buffer)//Função de dar Item.
    {   
        Buffer = itenInHand.GetComponent<IngredientInstance>();
        itenInHand = null; 
        hasItem = false;
        return Buffer;
    }


    public void ReceiveItens(_InteractionOBJ interactedObj)//Função de receber itens, recebendo o item como parâmetro.
    {

            itenInHand = interactedObj.GiveItens(interactedObj.itenItHas);
            if(itenInHand != null) hasItem = true;
       
    }
   

}
