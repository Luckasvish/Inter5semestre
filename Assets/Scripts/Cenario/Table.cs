using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Table : Interactable
{
    public  override InteractableType type { get; set; }
    public override Item itenItHas {get;set;}
    public override bool hasItemOnIt {get;set;}
    public override bool highLightOn{get; set;}
    public Transform[] platePosition;
    internal Item[] plates;

    public override Material OriginalMaterial {get; set;}
    
    public override Material FlashMaterial{get ; set;}
    
    public override MeshRenderer mesh{get; set;}
  
    //internal bool isFull;

    public Chair[] places;

    void Awake()
    {
        type = InteractableType._Table;
        itenItHas = null;
        hasItemOnIt = false;
        highLightOn = false;
        plates = new Item[2];
        mesh = this.GetComponent<MeshRenderer>();
        OriginalMaterial = mesh.material;
        FlashMaterial = MacroSistema.sistema.flashMaterial;
    }

     public override void ToogleHighLight(bool On)
    {

        if(On)
        {
            mesh.material = FlashMaterial;
        }
        else
        {
             mesh.material = OriginalMaterial;
        }

    }

    public override Item GiveItens (Item itenToGive)
    {
        Item Buffer;
        
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
            RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
            return Buffer;
        }
        else return null;
    }

    public override void ReceiveItens(Item item)
    {   
        if(places[0].client != null || places[1].client != null ) 
        
        {
            if(places[0].client != null && plates[1] == null)
            {
                if(places[0].client.clientOrder ==item.itemName)
                 {  
                    plates[0] = item;
                    plates[0].transform.position =  platePosition[0].position;
                    places[0].ReceiveItens(item);
                 }
                 else
                 {
                    plates[0] = item;
                    plates[0].transform.position =  platePosition[0].position;
                    places[0].ReceiveItens(item);
                    Debug.Log("places[0].client.clientOrder");
                    Debug.Log("Essa não é a receita que o cliente pediu!!!");
                 }
            
            }

            else if (places[1].client != null && plates[0] == null)
            { 
                 if(places[1].client.clientOrder == item.itemName)
                 {  
                    plates[1] = item;
                    plates[1].transform.position =  platePosition[1].position;
                    places[1].ReceiveItens(item);
                 }
                 
                 else
                 {
                    plates[1] = item;
                    plates[1].transform.position =  platePosition[1].position;
                    places[1].ReceiveItens(item);
                     Debug.Log("Essa não é a receita que o cliente pediu!!!");
                 }
        
            }
            
            else if(places[0].client != null && places[1].client != null)
            {
                
                if(places[0].client.clientOrder == item.itemName)
                 {  
                    plates[0] = item;
                    plates[0].transform.position =  platePosition[1].position;
                    places[0].ReceiveItens(item);
                 }
                 
                 else if(places[1].client.clientOrder == item.itemName)
                 {  
                    plates[1] = item;
                    plates[1].transform.position =  platePosition[1].position;
                    places[1].ReceiveItens(item);
                 }
                 
                 else {
                    plates[0] = item;
                    plates[0].transform.position =  platePosition[1].position;
                    places[0].ReceiveItens(item);
                     Debug.Log("Essa não é a receita que o cliente pediu!!!");
                 }


            }


        }
        else if(places[0].client == null && places[1].client == null)
        {

            if(plates[0] == null)
            {
                plates[0]  = item;
                plates[0].transform.position =  platePosition[0].position;
            }
            else if(plates[1] == null)
            {
                plates[1]  = item;
                plates[1].transform.position =  platePosition[1].position;
            }    

        }
        RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_put");

    }
    
    public override void Interact(Item iten, Chef chef)
    {
        if(iten != null)
        {
            ReceiveItens(chef.GiveIten(iten));
        }

        else 
        {
            if(places[0].client != null) 
            {

                if(places[0].client.GetActualBehaviour() == IBehaviour.BehaviourState.WaitingForOrder)
                {
                    TakeOrder(places[0].client);
                
                    Debug.Log("ChegouA!!!");//////////
                }
            }

            if(places[1].client != null)
            {

                if(places[1].client.GetActualBehaviour() == IBehaviour.BehaviourState.WaitingForOrder)
                {
                    TakeOrder(places[1].client);
                    Debug.Log("ChegouB!!!");//////////

                }
            }
            
            else if (places[0].client == null && places[1].client == null)
            {
                chef.ReceiveItens(this);
            }
        }

    }
    
    int CheckSeats()
    {
        if (places[0].client != null && places[1].client != null) return 2;
        else if (places[0].client != null && places[1].client == null) return -1;
        else if (places[0].client == null && places[1].client != null) return 1;
        else return 0;
    }

    void TakeOrder(Client client){ client.Ordering(); }

}
