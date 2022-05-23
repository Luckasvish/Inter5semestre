using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    static int actualLevel;
    static int payedRecipesNumber;
    static int nonPayedRecipesNumber;
    static int score;

    int startTimerNumber = 5 * 60;
    int endTimerNumber = 0;
    int actualTimerNumber;

    public bool isFinished = false;
    public GameObject[] StageComplete = new GameObject[3];

    SceneManage sceneManager;


    // Start is called before the first frame update
    void Start()
    {
        actualTimerNumber = startTimerNumber;
        sceneManager = GetComponent<SceneManage>();
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
        nonPayedRecipesNumber += increaseNonPayedRecipes;
    }

    public int GetNonPayedRecipes()
    {
        return nonPayedRecipesNumber;
    }

    public int GetScore()
    {
        return score;
    }

    void ChangeScoreAmount(int scoreToIncrease)
    {
        score = scoreToIncrease;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        actualTimerNumber -= 1;
        if (actualTimerNumber > 0)
        {
            StartCoroutine(Timer());
        }
        else
        {
            isFinished = true;
            if(sceneManager.GetLastScene() != sceneManager.GetActualScene())
            {
                StageComplete[0].SetActive(true);
            }
            else
            {
                if(Bank.instance.GetaActualMoney() == Bank.instance.GetGoalNumber())
                    StageComplete[1].SetActive(true);
                else 
                    StageComplete[1].SetActive(true);
            }


        }
    }
}
