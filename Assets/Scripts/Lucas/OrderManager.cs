using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public List<string> recipesToProduce;
    public List<string> client;
    void Start()
    {
        recipesToProduce = new List<string>();
    }

    void AddRecipeToList(string recipeToAdd)
    {
        recipesToProduce.Add(recipeToAdd);
    }

    void RemoveRecipeInList(string recipeToRemove)
    {
        recipesToProduce.Remove(recipeToRemove);
    }

    void ClearRecipeList()
    {
        recipesToProduce.Clear();
    }
}