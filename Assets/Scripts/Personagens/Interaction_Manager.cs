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
      interaction.Interact(chef.itenInHand, chef);
    }


    void InteractToBalcon(Interactable interaction) /// INTERAÇÃO COM O BALCÃO  
    {
        interaction.Interact(chef.itenInHand, chef);
    }

    void InteractToPreparer(Interactable interaction)   /// INTERAÇÃO COM O PREPARADOR
    {
      interaction.Interact(chef.itenInHand , chef);
    }

    void InteractToOven(Interactable interaction)
    {
        interaction.Interact(chef.itenInHand, chef);
    }

    void InteractToTable(Interactable interaction)
    {
       interaction.Interact(chef.itenInHand , chef);

    }

    InteractableType SetInteractionType(Interactable interaction)
    { 
        return interaction.type;
    }
}
