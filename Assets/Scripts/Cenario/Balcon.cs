using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balcon : Interactable
{
    public override InteractableType type { get; set;}

    public override Item itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    public override GameObject highLight { get ; set ; }
    public override bool highLightOn {get; set;}
    public Transform itemPosition;
    void Awake()
    {
        type = InteractableType._Balcon;
        if(GetComponentInChildren<Plates>() == null)
        {
            itenItHas = null;
        }
         highLight = GetComponentInChildren<Light>().gameObject;
        highLight.SetActive(false);
    }

    void Update()
    {
        if(hasItemOnIt)
        {
            itenItHas.transform.position = itemPosition.position;
        }
        if(highLightOn)
      {
        highLight.SetActive(true);
      }
      else 
      {
        highLight.SetActive(false);
      }
    }


    public override void ReceiveItens(Item itenInHand)
    {
        itenItHas = itenInHand;
        itenItHas.transform.position = itemPosition.position;
        hasItemOnIt = true;
    }

    public override Item GiveItens(Item itenToGive)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {
        itenToGive = itenItHas;
        itenItHas = null;
        hasItemOnIt = false;
        return itenToGive;
    }

}
