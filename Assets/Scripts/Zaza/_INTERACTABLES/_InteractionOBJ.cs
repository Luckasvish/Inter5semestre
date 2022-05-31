using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _InteractionOBJ : MonoBehaviour  // CLASSE MÃE DE INTERAGÍVEIS.
{
    //Propriedades Nativas de interagíveis.
    public  abstract InteractableType type { get; set; } // Tipo do interagível.
    public abstract _Item itenOnThis {get;set;}   //  Item no interagível.
    public abstract bool hasItemOnIt {get;set;}  //  Booleana para checar se o interagível tem um item nele ou não. 
    internal abstract Material material {get ; set;}   //   Referência pro material com Shader de Highlight.
  

    /////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////
    //Métodos Nativos de Interagíveis.


    public abstract _Item GiveItens(_Item itenToGive);    //  Método para dar item.
    public abstract void ReceiveItens( _Item itenReceived);   //  Método para receber item. 
    public abstract void Interact(_Item itenInHand, PJ_Character chef);  //  Método para lidar com as interações.

}

