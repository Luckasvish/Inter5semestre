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
    public Plates[] plates;
  
    internal bool isFull;

    public Chair[] places;

    void Awake()
    {
        type = InteractableType._Table;
        itenItHas = null;
        hasItemOnIt = false;
        highLightOn = false;
        
        Debug.Log(places.Length);
    }


    public void CheckPlaces()/////
    {

    }


    public override Item GiveItens (Item itenToGive)
    {
            Plates Buffer;
        if(plates[0] != null)
        {
            Buffer = plates[0];
            plates[0] = null;
            return Buffer;
        }
        else if (plates[1] != null)
        {
            Buffer = plates[1];
            plates[1] = null;
            return Buffer;
        }
        else return null;
    }

    public override void ReceiveItens(Item itenReceived)
    {   
        Plates plate = itenReceived.GetComponent<Plates>();
        if(places[0].client != null || places[1].client != null ) 
        
        {
            if(places[0].client != null && plates[0] == null)
            {
                plates[0] = plate;
            }
            else if (places[1].client != null && plates[1] == null)
            { 
                plates[1] = plate;
            }
        }
        else if(places[0].client == null && places[1].client == null)
        {

            if(plates[0] == null)
            {
                plates[0]  = plate;
                plates[0].transform.position =  platePosition[0].position;
            }
            else  if(plates[1] == null)
            {
                plates[1]  = plate;
                plates[1].transform.position =  platePosition[1].position;
            }    

        }




    }
    


}
