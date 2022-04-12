using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Plates : Itens
 
 {

    public  override ItenType type { get; set; }
    public  override string itemName { get; set; }
    public Transform pl_FoodPosition;

    internal bool hasItem;
    internal Itens itenItHas;


    void Awake()
    {
        Balcon balcon = GetComponentInParent<Balcon>();
        
        if(balcon != null)
        {
            balcon.itenItHas = GetComponent<Plates>();
            balcon.hasItemOnIt = true;
        }
        
       
        type = ItenType._Plate;
    }

    public void ReceiveRecipe(Recipes recipeForPlate)
    {
        itenItHas = recipeForPlate;
        hasItem = true;
        itenItHas.transform.position = pl_FoodPosition.position;
    }
    
 }