using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Interactable
{
    public  override InteractableType type { get; set; }
    public override Item itenItHas {get;set;}
    public override bool hasItemOnIt {get;set;}
    public override bool highLightOn{get; set;}
    public Transform[] platePosition;
    public Plates plate0;
    public Plates plate1;
    internal bool isFull;

    void Awake()
    {
        type = InteractableType._Table;
        itenItHas = null;
        hasItemOnIt = false;
        highLightOn = false;
    }

    public override Item GiveItens(Item itenToGive)
    {
       if(plate0 != null)
       {
           return plate0;
       }
       else if(plate1 != null)
                                {  return plate1;}
       else
       {
            return null;
       }
    }

    public override void ReceiveItens(Item itenReceived)
    {   
        if(plate0 == null)
        {
            plate0 = itenReceived.GetComponent<Plates>();
            plate0.transform.position =  platePosition[0].position;
        }
        else if(plate1 == null) 
        {
             plate1 = itenReceived.GetComponent<Plates>();
             plate1.transform.position =  platePosition[1].position;
        }
        else
        {
            Debug.Log("A mesa está cheia!");
        }      

    }
    
}
