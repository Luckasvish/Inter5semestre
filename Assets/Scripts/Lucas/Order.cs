using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Order
{
    //List<string> recipesToProduce;
    public string recipe;
    public string client;
    public string chair;

    public void GetRecipe(string recipe_)
    {
        recipe = recipe_;
    }    

}
public class OrderList : EventArgs
{
    public bool hasFood;
    public string recipe;
    public enum ItemType { Comida, Bebida };
    public OrderList(bool hasFood, string recipe)
    {
        this.hasFood = hasFood;
        this.recipe = recipe;
    }

}
