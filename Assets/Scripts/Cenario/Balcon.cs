using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balcon : MonoBehaviour
{
    public Transform itemPosition;
    internal bool hasItemOnIt;
    public Itens itenOnIt;

    void Update()
    {
        if(hasItemOnIt)
        {
            itenOnIt.transform.position = itemPosition.position;
        }
    }


    public void ReceivesItens(Itens itenInHand)
    {
        itenOnIt = itenInHand;
        itenOnIt.transform.position = itemPosition.position;
        hasItemOnIt = true;
    }

    public Itens GivesIten(Itens buffer)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {
        buffer = itenOnIt;
        itenOnIt = null;
        hasItemOnIt = false;
        return buffer;
    }

}
