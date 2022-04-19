using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;
    public List<string> recipesToProduce;
    int recipeIndex;
    public OrderHUD[] OrderInHud;




    private void Awake()
    {
        instance = this;
        for(int i = 0; i < OrderInHud.Length;i++)
        {
            OrderInHud[i].gameObject.SetActive(false);
        }

    }
    void Start()
    {
        recipesToProduce = new List<string>();
    }

    public void AddRecipeToList(string recipeToAdd)
    {
        recipesToProduce.Add(recipeToAdd);
        OrderInHud[0].SetReciepOrderHUD(recipeToAdd);
        OrderInHud[0].gameObject.SetActive(true);
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