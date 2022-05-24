using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;
    public GameObject[] recipesHud = new GameObject[16];
    public Image[] recipesHudSprites = new Image[16];

    public List<string> recipesToProduce;
    int recipeIndex;

    [SerializeField]
    Image[] OrderImages = new Image[3];

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < recipesHud.Length; i++)
        {
            recipesHudSprites[i] = recipesHud[i].GetComponent<Image>();
            recipesHud[i].SetActive(false);
        }
    }
    void Start()
    {
        recipesToProduce = new List<string>();
    }

    public int AddRecipeToList(string recipeToAdd)
    {
        recipesToProduce.Add(recipeToAdd);
        RecipeInHud(true, recipeIndex);
        recipeIndex += 1;
        return recipeIndex - 1;
    }    
    
    public void RecipeInHud(bool isAdding, int _recipeIndex)
    {
        if (isAdding)
        {
            recipesHudSprites[_recipeIndex].sprite = GetOrderImage(recipesToProduce[recipeIndex]);
            recipesHud[_recipeIndex].SetActive(true);
        }
        else
            recipesHud[_recipeIndex].SetActive(false);
    }
    public Sprite GetOrderImage(string _clientOrder)
    {
        int _recipeindex = 0;
        switch (_clientOrder)
        {
            case "Feijoada":
                _recipeindex = 0;
                break;
            case "PratoFeito":
                _recipeindex = 1;
                break;
            case "Buchada":
                _recipeindex = 2;
                break;
        }
        return OrderImages[_recipeindex].sprite;
    }

    public void RemoveRecipeInList(int _recipeIndex, string recipeToAdd)
    {
        recipesToProduce.Remove(recipeToAdd);
        RecipeInHud(false, _recipeIndex);
        recipeIndex -= 1;
    }

    public void ClearRecipeList()
    {
        recipesToProduce.Clear();
        recipeIndex = 0;
    }
}