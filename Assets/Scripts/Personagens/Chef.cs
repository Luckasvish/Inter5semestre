using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    ///COMPONENTES DE PLAYER///
    internal Movement_Manager movement_Manager;
    internal Input_Manager input_Manager;
    internal Interaction_Manager interaction_Manager;
    internal Detection_Manager detection_Manager;
    ///--------------------///


    ///CHEF ---
    public Itens itenInHand;
    internal bool hasItem;
    public Transform itenPosition;



    void Awake()
    {
        input_Manager = MacroSistema.sistema.input_Manager;
        movement_Manager = GetComponent<Movement_Manager>();
        interaction_Manager = GetComponent<Interaction_Manager>();
        detection_Manager = GetComponent<Detection_Manager>();
    }


    void Update()
    {
        if(detection_Manager.canInteract == true)
        {
            if (input_Manager.pressedE == true)
            {
                interaction_Manager.Interaction(detection_Manager.interactable);
            }
        }
 
        if(hasItem == true)
        {
            itenInHand.transform.position = itenPosition.position;
        }

    }

    void FixedUpdate()
    {
        movement_Manager.Move(input_Manager.moveInput);
    }

    internal Itens GiveIten(Itens Buffer)//Função de dar Item.
    {   
        Buffer = itenInHand;
        itenInHand = null; 
        hasItem = false;
        return Buffer;
    }

    public void ReceiveItens(Interactable interactedObj)//Função de receber itens, recebendo o item como parâmetro.
    {

            itenInHand = interactedObj.GiveItens(interactedObj.itenItHas);
            hasItem = true;
       
    }


}
