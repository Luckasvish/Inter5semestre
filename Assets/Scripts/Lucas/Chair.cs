using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
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
