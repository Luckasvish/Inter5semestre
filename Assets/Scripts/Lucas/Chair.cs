using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public static Chair instance;
    public GameObject Food;

    private bool hasItem;
    private bool hasFood;
    private bool hasDrink;

    private string itemOrdered;
    string itemReceived;

    Item item;



    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIfHasFood()
    {
        return hasFood; 
    }

    public bool CheckFood()
    {
        if(itemOrdered == itemReceived)
            return true;
        else 
            return false;
    }



    public void GetOrder(string order)
    {
        itemOrdered = order;
    }

    void ReceiveItens(Item item)
    {
        hasItem = true;
        this.item = item.GetComponent<Plates>().itenItHas;
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

}
