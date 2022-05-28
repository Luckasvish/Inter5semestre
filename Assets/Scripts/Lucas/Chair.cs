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

    _Item item;
    internal Client client;

    internal Table thisTable;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        thisTable = GetComponentInParent<Table>();
    }

    public bool CheckIfHasFood()
    {
        return hasFood; 
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
        Debug.Log("itemOrdered: " + clientOrder);
    }

    public void ReceiveItens(_Item _item)
    {
        hasItem = true;
        this.item = _item;
        
        if(item.type == ItemType._Plate)
        {   

            Debug.Log("Essa cadeira recebeu um prato : " + item.itemName);/////
            hasFood = true;    
            Debug.Log("HasFood " + hasFood);////
            itemReceived = item.itemName;
            Food = item.gameObject;

        }

        else
        {
            Debug.Log("Esse item aí não é um prato não !!!");/////////;
        }
    }


    public void CleanPlate()
    {
        if(client != null)
        {
            Plates p = item.GetComponent<Plates>();
            Debug.Log("Item: " +item);//////////
            if(p != null)
            {
                p.CleanPlate();
                 Debug.Log("Prato: " + p);//////////
            }
        }

    }

    public _Item GiveItem(bool changeStatus,_Item buffer)
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
        //CleanPlate();
        client = null;
    }

}
