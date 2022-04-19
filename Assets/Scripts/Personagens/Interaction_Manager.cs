using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Manager : MonoBehaviour
{
        Chef chef;
        void Awake()
    {
        chef = GetComponent<Chef>();
    }

    public void Interaction(Interactable interaction)
    {
    
        switch(SetInteractionType(interaction))
        {
            case InteractableType._Storer: InteractToStorer(interaction);
            break;
            case InteractableType._Preparer: InteractToPreparer(interaction);
            break;
            case InteractableType._Oven: InteractToOven(interaction); 
            break;
            case InteractableType._Balcon: InteractToBalcon(interaction);
            break;
            case InteractableType._Table: InteractToTable(interaction);
            break;
            default: Debug.Log("Não dá pra interagir!!");///
            break;

        }
    }
    void InteractToStorer(Interactable interaction) /// INTERAÇÃO COM O STORER
    {
        if(chef.hasItem)    //000 PRIMEIRA CHECAGEM: SE O JOGADOR TEM ITEM 
        {
            Debug.Log("Você já tem um item na mão");
        }

        else 
        {
           chef.ReceiveItens(interaction);
        }

    }


    void InteractToBalcon(Interactable interaction) /// INTERAÇÃO COM O BALCÃO  
    {

        if(chef.hasItem)        //000 PRIMEIRA CHECAGEM: SE O JOGADOR TEM ITEM 
        {
            if(chef.itenInHand.type == ItemType._Pan)
            {
                if(interaction.itenItHas.type == ItemType._Plate)
                {
                    Plates plate = interaction.itenItHas.GetComponent<Plates>();
                    Pan pan = chef.itenInHand.GetComponent<Pan>();

                    if(plate != null && pan != null)
                    {

                        plate.ReceiveIngredient(pan.GiveItem(pan.ingredient.ingredient.ingreType));
                    }
                    

                }
            }

            if(interaction.hasItemOnIt) // 001 SEGUNDA CHECAGEM: SE O BALCÃO TEM ITEM 
            {
                Debug.Log("Já tem um item neste balcão!!!");/////
            }
            else
            {
                interaction.ReceiveItens(chef.GiveIten(chef.itenInHand));
            }

        }

        else                    //000 VOLTANDO A PRIMEIRA CHECAGEM: SE O JOGADOR NÃO TIVER ITEM 
        {   

            if(interaction.hasItemOnIt)   // 001 SEGUNDA CHECAGEM: SE O BALCÃO TEM ITEM 
            {
                    chef.ReceiveItens(interaction);
            }
            else
            {
                Debug.Log("Não tem item aqui!");///
            }
        }

    }

    void InteractToPreparer(Interactable interaction)   /// INTERAÇÃO COM O PREPARADOR
    {
        Preparer preparer = interaction.GetComponent<Preparer>(); /// REF PRA TRATAR O PREPARADOR

        if(chef.hasItem) //000 PRIMEIRA CHECAGEM: SE O JOGADOR TEM ITEM 
        {
        
           if(preparer.hasItemOnIt == false) //001 SEGUNDA CHECAGEM: SE O PREPARADOR TEM ITEM 
            {
                
                if(chef.itenInHand.type == ItemType._UnpreparedIngredient)
                {
                    preparer.ReceiveItens(chef.GiveIten(chef.itenInHand));
                }
                else 
                {
                    Debug.Log("Esse item não vai aqui!!");///
                };

            }
            else 
            {
                Debug.Log("Já tem um item aqui!!!");
            }
        }
        else  //000 PRIMEIRA CHECAGEM: SE O JOGADOR TEM ITEM 
        {
            if(preparer.hasItemOnIt)     //001 SEGUNDA CHECAGEM: SE O PREPARADOR TEM ITEM 
            {
                if(preparer.itenItHas.type != ItemType._PreparedIngredient)  //002 TERCEIRA CHECAGEM: SE O ITEM ESTÁ PRONTO 
                {
                    preparer.TooglePreparer();
                }
                else 
                {
                    chef.ReceiveItens(preparer);
                }
            }
            
            else 
            {
                Debug.Log("Não tem ingrediente aqui!");///
            }
        }
    }

    void InteractToOven(Interactable interaction)
    {
        Oven oven = interaction.GetComponent<Oven>();
        Pan pan = oven._Pan;
       

        
        if (chef.hasItem)
        {    ItemType itenType = chef.itenInHand.type;
            

            switch(itenType)
            {

                case ItemType._PreparedIngredient:
                IngredientInstance ingre = chef.itenInHand.GetComponent<IngredientInstance>();

                    if(oven.hasItemOnIt == true)
                    {
                        pan.ReceiveItens(chef.GiveIten(ingre));
                    }
                    else 
                    {
                        Debug.Log("Está faltando a panela !!!"); ///////////////////////////////////////////////////////////////////////////////////////////  
                    }
                                                    break;
                
                case ItemType._Plate:
                Plates plate =  chef.itenInHand.GetComponent<Plates>();
                    
                    if(oven.hasItemOnIt == true)
                    {
                        if(pan.ready.activeInHierarchy && plate.recipe != null)
                        {
                            plate.ReceiveIngredient(pan.GiveItem(pan.ingredient.ingredient.ingreType));
                        }

                        else
                        {
                            Debug.Log("Ainda não está pronto"); /////////////////////////////////////////////////////////////////////////////////////////// 
                        }
                    }
                    else 
                    {
                        Debug.Log("Está faltando a panela !!!"); ///////////////////////////////////////////////////////////////////////////////////////////  
                    }

                                                    break;

                case ItemType._Pan:

                    if(oven.hasItemOnIt == false)
                    {
                        oven.ReceiveItens(chef.GiveIten(chef.itenInHand));
                    }
                    else 
                    {
                        Debug.Log("Já tem panela aqui"); /////////////////////////////////////////////////////////////////////////////////////////// 
                    }
                                                    break;

                default : Debug.Log("Deu ruim!!"); break;

            }
        }    

        else 
        {
            if(oven.hasItemOnIt)
            {
                chef.ReceiveItens(oven);
            }
        } 
    }

    void InteractToTable(Interactable interaction)
    {
        Table table = interaction.GetComponent<Table>();

        if(table.places[0].client != null || table.places[1].client != null )
        {






            
        }
        
        else
        {
                if(chef.hasItem)
                {
                    if(chef.itenInHand.type == ItemType._Plate)
                    {
                            if(table.plates[0] != null && table.plates[1] != null)
                            {
                                Debug.Log("Já tem muito prato aqui!!!");/////////////////////////////////////////////////////
                            }
                            else if (table.plates[0] == null || table.plates[1] == null)
                            {
                                table.ReceiveItens(chef.GiveIten(chef.itenInHand));
                            }
                    }
                }
                else 
                {
                    if(table.plates[0] != null || table.plates[1] != null)
                    {
                        chef.ReceiveItens(table);
                    }
                    else
                    {
                        Debug.Log("Não tem nada aqui!!!!");////////////////////////////////////////////       
                    }
                }
                
        }




        if(chef.hasItem)
        {
            if(chef.itenInHand.type == ItemType._Plate)
            {
                Plates plate = chef.itenInHand.GetComponent<Plates>();
                if(table.isFull == false)
                {
                    table.ReceiveItens(chef.GiveIten(plate));
                }
                else
                {
                    Debug.Log("A mesa está cheiaa !!!!!!!");/////////////////////////////////
                }
            }
            else 
            {
                 Debug.Log("Esse item não vai aqui!");/////////////////////////////////
            }

        }
        else
        {

            if(table.places[0].client != null || table.places[1].client != null )
            {     
                Client client;

                if(table.places[0].client != null) client = table.places[0].client ;
                else if(table.places[1].client != null) client = table.places[1].client ;
                else client = null;

                Debug.Log("ClientNaMesa:"+ client);///

                switch(client.behaviourState)
                {
                    case IBehaviour.BehaviourState.WaitingForOrder: 
                        
                            client.hasOrdered = true;
                            Debug.Log(client.hasOrdered);
                    
                        break;
                    default :Debug.Log("O cliente está em outro estado que não o WaitingFor Order"); //////////////////////////
                     break;
                }
            }
            else
            {

                if(table.plates[0] != null || table.plates[1] != null)
                {
                    chef.ReceiveItens(table);
                }
                else
                {
                    Debug.Log("Não tem nada na mesa!");///////////////////////////////////////////
                }

            }

        }





    }

    InteractableType SetInteractionType(Interactable interaction)
    { 
        return interaction.type;
    }
}
