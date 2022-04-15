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

                    if(pan.ingreIn < 3)
                    {
                        pan.CheckIngredient(chef.GiveIten(chef.itenInHand));
                    }
                    else 
                    {
                        Debug.Log("A panela está cheia !!");///
                    }

                break;

                case ItemType._Pan: 
                    
                    if(oven.hasItemOnIt == false)
                    {
                        oven.ReceiveItens(chef.GiveIten(chef.itenInHand));
                    }
                    else
                    {
                        Debug.Log("Esse item não vai aqui!!");///
                    }
                break;

                case ItemType._Plate:
                    Plates plate = chef.itenInHand.GetComponent<Plates>();

                    if(pan.recipe)
                    {
                        pan.GiveRecipe(plate);
                    }
                    
                    else 
                    {
                        Debug.Log("A receita ainda não está pronta");
                    }
                    

                break;

                default: Debug.Log("Esse item não vai aqui !!!");///
                break;
            }

        }
        else 
        {
            if(oven.hasItemOnIt)
            {
                chef.ReceiveItens(oven);
            }
            else 
            {
                Debug.Log("Não tem panela aqui!");///
            }
        }

    }

    InteractableType SetInteractionType(Interactable interaction)
    { 
        return interaction.type;
    }
}
