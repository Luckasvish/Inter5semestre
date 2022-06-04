using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Table : _InteractionOBJ
{
   // PROPRIEDADES DE INTERACTIONOBJ
    public override _Item itenOnThis {get;set;} = null;
    public override bool hasItemOnIt {get;set;} = false;
    internal override bool blinking{get ; set;}  
    internal override float blinkTimer {get; set;}
    //////////////////////////////////////////////////////
    
    
    // PROPRIEDADES DE TABLE 
    public Transform[] platePosition;
    internal _Item[] plates = new _Item[2];
    public Chair[] places;

     public Renderer renderers;    
      public float blinkTime;

    void Update()
    {
        if(blinking == true) Blink();
    }

     public void Blink()
    {
        blinkTimer += Time.deltaTime;
        renderers.material.SetInt("_BlinkOn" , 1);

        if(blinkTimer >= blinkTime)
        {
            StopBlinking();
        }

    }

    public void StopBlinking()
    {
        renderers.material.SetInt("_BlinkOn" , 0);
        blinking = false;
        blinkTimer = 0;
    }



    //Método para checar se tem clientes na mesa : retorna true caso haja pelo menos 1.
    bool CheckClients(){return (places[0].client != null || places[1].client != null) ? true : false;}
    
    bool CheckIfIsFull(){return (plates[0] == null || plates[1] == null) ? false : true;}
    
    bool GetClientState(Client chair){return (chair.GetActualBehaviour() == IBehaviour.BehaviourState.WaitingForOrder) ? true : false;}

    // Método para checar se ambos os lugares estão ocupados : retorna true caso ambos estejam.
    bool CheckBothPlaces()
    {
       bool hasClient1;
       bool hasClient2;
       hasClient1 = (places[0].client != null)?  true : false; 
       hasClient2 = (places[1].client  != null)?  true : false; 
       return  (hasClient1 && hasClient2)?  true : false; 
    }

    int CheckWhichPlace(){ return (places[0].client != null)?  0 : 1;}

    int CheckWhereToPutPlate(string platename)
    {
        int x = 0;
        for (int i = 0; i < places.Length ; i++)
        {
            if(places[i].clientOrder == platename) x = i;
            
        }

        return x;
    }
    void PositionPlate()
    {
        if(plates[0] != null) plates[0].transform.position = platePosition[0].position;

        if(plates[1] != null) plates[1].transform.position = platePosition[1].position;
    }

    void TreatClient(Client client , PJ_Character chef , _Item itenInHand)  ////////////////////////////////////////////////////---TREAT CLIENT---////////////////////////////////////////////////////////////////////////////
    {
        if(GetClientState(client)) TakeOrder(client);         
        else   
        {
            if(itenInHand != null) 
            {
              
                if(CheckIfIsFull() == false)    ReceiveItens(chef.GiveIten(itenInHand));   
                
                else 
                {
                    Debug.Log("A mesa está cheia!!!");
                    return;
                    }
                }

            else 
            {
                chef.ReceiveItens(this);  
            }
        }
    }

    bool CheckAnyPlates()
    {
        return (plates[0] != null || plates[1] != null)? true:false;
    }

    int CheckWhichPlate()
    {
        if(plates[0] != null) return 0;
        else return 1;
    }


    public bool CheckIfHasFood(Chair chair)
    {
        if(chair == places[0])
        {
            if(plates[0] != null) return true;
            else 
            { 
                return false;
            }
            
        }
        else if(chair == places[1])
        {
            if(plates[1] != null) return true;
            else 
            {  
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool CheckIfClientCanEat(string clientOrder, Chair chair)
    {
        int i = (chair == places[0])? 0:1; 
        return (clientOrder == plates[i].itemName)? true : false;
    }

    public void CleanClientPlate(Chair chair)
    {
        int i = (chair == places[0])? 0:1; 
        plates[i].GetComponent<Plates>().CleanPlate();
        
    }


     public override void Interact(_Item itenInHand, PJ_Character chef)     ////////////////////////////////////////////////////---INTERACT---////////////////////////////////////////////////////////////////////////////
     {
        if(CheckClients())  
        {
            if(CheckBothPlaces())   
            {
               foreach(Chair c in places)
               {
                   TreatClient(c.client, chef, itenInHand);
               }

            }
            else   
            {
                TreatClient(places[CheckWhichPlace()].client, chef,itenInHand);
            }
        }

        else 
        {
            if(itenInHand != null)
            {
                if(CheckIfIsFull() == false)
                {
                    ReceiveItens(chef.GiveIten(itenInHand));
                }
                else 
                {
                    Debug.Log("A mesa está cheia!!!");
                    return;
                }
            }
            
            else chef.ReceiveItens(this);
            
        }

     }


        public override void ReceiveItens(_Item item)    ////////////////////////////////////////////////////---RECEIVE---////////////////////////////////////////////////////////////////////////////
    {
        
        plates[CheckWhereToPutPlate(item.itemName)] = item;        
        RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_put");
        PositionPlate();
        item.transform.SetParent(this.transform);
    }




    public override _Item GiveItens (_Item itenToGive)  ////////////////////////////////////////////////////---GIVE ITEM---////////////////////////////////////////////////////////////////////////////
    {
        _Item Buffer;
        

        if(CheckAnyPlates())
        {
                if(plates[CheckWhichPlate()].itemName != places[CheckWhichPlate()].client.clientOrder)
                {
                    Buffer = plates[CheckWhichPlate()];
                    plates[0] = null;
                    RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
                    return Buffer;
                }
                else return null;
        }
        else
        {
            Debug.Log("Não tem nenhum prato aqui!");//////////
            return null;
        }
    }
    
    void TakeOrder(Client client){ client.Ordering(); }

}
