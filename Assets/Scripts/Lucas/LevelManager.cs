using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    static int actualLevel;
    static int payedRecipesNumber;
    static int nonPayedRecipesNumber;
    static int score;

    int startTimerNumber = 5*60;
    int endTimerNumber = 0;
    [SerializeField]
    int actualTimerNumber;
    int secondsTimer = 0;

    [SerializeField]
    TextMeshProUGUI timerMinutesText;    
    [SerializeField]
    TextMeshProUGUI timerSecondsText;

    public static bool isFinished = false;


    // Start is called before the first frame update
    void Start()
    {
        actualTimerNumber = startTimerNumber;
        StartCoroutine(Timer());
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
        actualTimerNumber -= 1;

        if ((actualTimerNumber % 60) >= 10)
        { 
            timerMinutesText.text = (actualTimerNumber / 60).ToString() + ":";
            timerSecondsText.text = (actualTimerNumber % 60).ToString(); 
        }
        else
        {
            timerMinutesText.text = (actualTimerNumber / 60).ToString() + ": ";
            timerSecondsText.text = "0"+ (actualTimerNumber % 60).ToString(); 
        }

        if(actualTimerNumber <= 10)
        {
            timerSecondsText.fontSize = 80;
            timerSecondsText.color = Color.red;
            timerMinutesText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
        }
        else
            yield return new WaitForSeconds(0.5f);




        if (actualTimerNumber <= 10)
        {
            timerSecondsText.fontSize = 70;
        }
            yield return new WaitForSeconds(0.5f);
        if (actualTimerNumber > 0)
        {
            StartCoroutine(Timer());
        }
        else
        {
            isFinished = true;
            if(SceneManage.currentSceneIndex < 8)
            {
                SceneManage.GoToNextScene();
            }
            else
            {
                if(Bank.instance.GetaActualMoney() == Bank.instance.GetGoalNumber())
                    SceneManage.GoToScene("Win");
                else
                    SceneManage.GoToScene("Lose");
            }


        }
    }
}
