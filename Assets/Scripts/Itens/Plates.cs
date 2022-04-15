using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Plates : Item
 
 {

    public  override ItemType type { get; set; }
    public  override string itemName { get; set; }
    public Transform pl_FoodPosition;

    internal bool hasItem;
    internal Item itenItHas;


    void Awake()
    {
        Balcon balcon = GetComponentInParent<Balcon>();
        
        if(balcon != null)
        {
            balcon.itenItHas = GetComponent<Plates>();
            balcon.hasItemOnIt = true;
        }
        
       
        type = ItemType._Plate;
    }

    public void ReceiveRecipe(Recipes recipeForPlate)
    {
        itenItHas = recipeForPlate;
        hasItem = true;
        itenItHas.transform.position = pl_FoodPosition.position;
    }
    
 }