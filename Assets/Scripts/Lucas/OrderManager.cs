using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;
    public List<string> recipesToProduce;
    public List<string> client;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        recipesToProduce = new List<string>();
    }

    public void AddRecipeToList(string recipeToAdd)
    {
        recipesToProduce.Add(recipeToAdd);
    }

    public void RemoveRecipeInList(string recipeToRemove)
    {
        recipesToProduce.Remove(recipeToRemove);
    }

    public void ClearRecipeList()
    {
        recipesToProduce.Clear();
    }
}