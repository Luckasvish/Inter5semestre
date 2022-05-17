using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    static int actualLevel;
    static int payedRecipesNumber;
    static int nonPayedRecipesNumber;
    static int score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PayedRecipes(int increasePayedRecipes)
    {
        payedRecipesNumber += increasePayedRecipes;
    }

    public int GetPayedRecipes()
    {
        return payedRecipesNumber;
    }

    void NonPayedRecipes(int increaseNonPayedRecipes)
    {
        payedRecipesNumber += increaseNonPayedRecipes;
    }

    public int GetScore()
    {
        return score;
    }

    void ChangeScoreAmount(int scoreToIncrease)
    {
        score = scoreToIncrease;
    }
}
