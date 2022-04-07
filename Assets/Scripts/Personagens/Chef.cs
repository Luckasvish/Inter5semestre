using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    ///COMPONENTES DE PLAYER///
    internal Movement_Manager movement_Manager;
    internal Input_Manager input_Manager;

    [SerializeField]
    internal Detection_Manager detector;
    ///--------------------///


    ///CHEF ---
    public Itens itenInHand;
    bool hasItem;
    public Transform itenPosition;



    void Awake()
    {
        input_Manager = MacroSistema.sistema.input_Manager;
        movement_Manager = GetComponent<Movement_Manager>();
    }


    void Update()
    {
        bool actionButon = input_Manager.pressedE;
        Debug.Log(itenInHand);///
    
        if(actionButon)
        {      
            if (detector.canInteract)
            {
                HandleInteraction(detector.interagible);
            }                    
        }
        
        if(hasItem == true)
        {
            itenInHand.transform.position = itenPosition.position;
        }
        else
        {
            itenInHand = null;
        }
 
    }

    void FixedUpdate()
    {
        movement_Manager.Move(input_Manager.moveInput);
    }



    Itens GiveIten(Itens Buffer)//Função de dar Item.
    {   
        Buffer = itenInHand;
        itenInHand = null; 
        hasItem = false;
        return Buffer;
    }

    void ReceiveItens(Interagibles interactedObj)//Função de receber itens, recebendo o item como parâmetro.
    {

            itenInHand = interactedObj.GiveItens(interactedObj.itenItHas);
            hasItem = true;
       
    }


    void HandleInteraction(Interagibles interaction)
    {


        if(hasItem == false)
        {
            if(interaction.hasItemOnIt ==  true)
            {
                switch (interaction.type)
                {
                    case  InteragibleType._Storer :
                    interaction.GetComponent<Storers>().CountItenToSpawn();
                    ReceiveItens(interaction);
                    break;

                    case  InteragibleType._Preparer :
                        
                        if(interaction.GetComponent<Preparer>().prepared == true)
                        {
                            ReceiveItens(interaction);
                        }
                        else 
                        {
                            PrepareIngredient();
                        }
    
                    break;

                    case InteragibleType._Oven :

                        if(interaction.GetComponent<Oven>().cooked == false)
                        {
                            ReceiveItens(interaction);
                        }
                        else 
                        {
                           itenInHand = interaction.itenItHas.GetComponent<Pan>().GiveRecipe();
                        }

                    break;


                    default: ReceiveItens(interaction);

                    break;
                }
            }

            else 
            {
                Debug.Log("Não tem itens aqui!!!");
            }
        
        }

        else
        {
            switch(interaction.type)
            {
                case InteragibleType._Balcon : 
                
                    if(interaction.hasItemOnIt)
                    {
                        Debug.Log("Já há um item nesse balcão");
                    }
                    else
                    {
                        interaction.ReceiveItens(GiveIten(itenInHand));
                    }
                break;

                case InteragibleType._Storer : 
                
                Debug.Log("Você já está carregando outro item!");///
            
                break;

                case InteragibleType._Preparer :

                    if(interaction.hasItemOnIt)
                    {
                        Debug.Log("Já há um item nesse preparador");
                    }
                    else
                    {
                        interaction.ReceiveItens(GiveIten(itenInHand));
                    }  
            
                break;

                case InteragibleType._Oven :

                    if(interaction.hasItemOnIt)
                    {
                        if(itenInHand.type != ItenType._PreparedIngredient)
                        {
                            Debug.Log("Esse item não está preparado!");///
                        }
                    
                        else
                        {
                            interaction.GetComponent<Oven>().PutIngredient(GiveIten(itenInHand));
                        }
                    }

                    else 
                    {
                        if(itenInHand.type != ItenType._Pan)
                        {
                            Debug.Log("Aí não é lugar pra isso aí!");///
                        }
                        else 
                        {
                            interaction.ReceiveItens(GiveIten(itenInHand));
                        }
                    }
                
                break;

            }

        }

    }

    void PrepareIngredient()
    {


    }



}
