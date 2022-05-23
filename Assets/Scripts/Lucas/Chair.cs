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

    string clientOrder;
    string itemReceived;

    Item item;
    internal Client client;

    private void Awake()
    {
        instance = this;
    }


    public bool CheckIfHasFood()
    {
        return hasFood; 
    }

    public bool CheckFood()
    {
        if(clientOrder == itemReceived)
        {
            Debug.Log("itemOrdered: " + clientOrder);
            Debug.Log("itemReceived: " + itemReceived);
            return true;
        }
        else 
            return false;
    }



    public void GetOrder(string order)
    {
        clientOrder = order;
        Debug.Log("itemOrdered: " + clientOrder);
    }

    public void ReceiveItens(Item _item)
    {
        hasItem = true;
        this.item = _item.GetComponent<Plates>();
        if(item.type == ItemType._Plate)
        {   
            hasFood = true;    
            itemReceived = item.name;
        }
    }

    public Item GiveItem(bool changeStatus,Item buffer)
    {
        buffer = item;
        item = null;
        hasItem = changeStatus;
        hasFood = changeStatus;
        hasDrink = changeStatus;
        itemReceived = "";
        return buffer;
    }

    public void ClientGetOff()
    {
        client = null;
    }

}
