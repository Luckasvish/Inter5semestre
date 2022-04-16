using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Order: MonoBehaviour
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
