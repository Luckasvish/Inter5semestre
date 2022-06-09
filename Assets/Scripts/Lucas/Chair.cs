using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : _InteractionOBJ
{

  public override _Item itenOnThis {get;set;}   //  Item no interagível.
    public override bool hasItemOnIt {get;set;}  //  Booleana para checar se o interagível tem um item nele ou não. 
    internal override bool blinking {get ; set;}   //   Referência pro material com Shader de Highlight.
    internal override float blinkTimer {get; set;}
    
     public Renderer renderers;    
     public float blinkTime;
       public override _Item GiveItens(_Item itenToGive)
   {
       return null;
   }    //  Método para dar item.
    public override void ReceiveItens( _Item itenReceived)
    {

    }  //  Método para receber item. 
    public override void Interact(_Item itenInHand, PJ_Character chef)
    {
        if(client != null)
        {
            if(client.GetActualBehaviour() == IBehaviour.BehaviourState.WaitingForOrder)
            {
                client.Ordering();
            }
            else
            {
                return;
            }
        }

    }  //  Método para lidar com as interações.


    /////////////////////////////////////////////////////
    public static Chair instance;
    public GameObject Food;
    bool hasItem;
    bool hasFood;
    bool hasDrink;

    internal string clientOrder;
    string itemReceived;

    _Item item;
    internal Client client;

    internal Table thisTable;
    internal Transform clientPosition;
    [SerializeField]
    Transform TableDirection;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        thisTable = GetComponentInParent<Table>();
        clientPosition = GetComponentInChildren<Transform>();
    }

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

    public bool CheckFood()
    {
        Debug.Log("itemOrdered: " + clientOrder);
        Debug.Log("itemReceived: " + itemReceived);
        
        if(clientOrder == itemReceived)
        {
            return true;
        }
        else 
            return false;
    }



    public void GetOrder(string order)
    {
        clientOrder = order;
//        Debug.Log("itemOrdered: " + clientOrder);
    }
 
    public void ClientGetOff(){client = null;}

    public Vector3 PositionToLook()
    {
        return TableDirection.position;
    }
}
