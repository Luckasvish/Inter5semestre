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

    int startTimerNumber = 8 * 60;
    int endTimerNumber = 18 * 60;
    [SerializeField]
    int actualTimerNumber;
    int fakeActualTimerNumber =1;
    int secondsTimer = 0;

    [SerializeField]
    TextMeshProUGUI timerMinutesText;    
    [SerializeField]
    TextMeshProUGUI timerSecondsText;

    public static bool isFinished = false;
    bool canChangeScene = false;


    // Start is called before the first frame update
    void Start()
    {
        isFinished = false;
        canChangeScene = false;
        actualTimerNumber = startTimerNumber;
        timerMinutesText.text = "0" + (((actualTimerNumber / 2) / 30) % 24).ToString() + ": ";
        timerSecondsText.text = " 0" + (actualTimerNumber % 30).ToString();
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
        if (!SpawnManager.instance._TutorialEnded)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(Timer());
            yield break;
        }
        actualTimerNumber += 2;
        int timer_seconds = actualTimerNumber - 1;
        if ((actualTimerNumber % 60) >= 10)
        {
            if ((((actualTimerNumber / 2) / 30) % 24) < 10 && (((actualTimerNumber / 2) / 30) % 24) != 60)
                timerMinutesText.text = "0" + (((actualTimerNumber / 2) / 30) % 24).ToString() + ": ";
            else
                timerMinutesText.text = (((actualTimerNumber / 2) / 30) % 24).ToString() + ": ";
            if (actualTimerNumber % 60 < 10 && timer_seconds % 60 != 59 || timer_seconds % 60 == 9)
                timerSecondsText.text = " 0" + (timer_seconds % 60).ToString();
            else
                timerSecondsText.text = " " + (timer_seconds % 60).ToString();
        }
        else
        {
            if(timer_seconds % 60 != 59)
                timerSecondsText.text = " 0" + (timer_seconds % 60).ToString();
            else
                timerSecondsText.text = " " + (timer_seconds % 60).ToString();
        }

        if (actualTimerNumber >= endTimerNumber - 10)
        {
            timerSecondsText.fontSize = 80;
            timerSecondsText.color = Color.red;
            timerMinutesText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
        }
        else
            yield return new WaitForSeconds(0.5f);


        if ((actualTimerNumber % 60) >= 10)
        {
            if ((((actualTimerNumber / 2) / 30) % 24) < 10)
                timerMinutesText.text = "0" + (((actualTimerNumber / 2) / 30) % 24).ToString() + ": ";
            else
                timerMinutesText.text = (((actualTimerNumber / 2) / 30) % 24).ToString() + ":";

            if (actualTimerNumber % 60 < 10 && actualTimerNumber % 60 != 59)
                timerSecondsText.text = " 0" + (actualTimerNumber % 60).ToString();
            else
                timerSecondsText.text = " " + (actualTimerNumber % 60).ToString();
        }
        else
        {
            if ((((actualTimerNumber / 2) / 30) % 24) < 10)
                timerMinutesText.text = "0" + (((actualTimerNumber / 2) / 30) % 24).ToString() + ": ";
            else
                timerMinutesText.text = (((actualTimerNumber / 2) / 30) % 24).ToString() + ":";

            if (actualTimerNumber % 60 < 10)
                timerSecondsText.text = " 0" + (actualTimerNumber % 60).ToString();
            else
                timerSecondsText.text = " " + (actualTimerNumber % 60).ToString();
        }

        if (actualTimerNumber >= endTimerNumber -20)
        {
            timerSecondsText.fontSize = 70;
        }
            yield return new WaitForSeconds(0.5f);
        if (actualTimerNumber < endTimerNumber)
        {
            StartCoroutine(Timer());
        }
        else
        {
            isFinished = true;
            StartCoroutine(FinishAnim());
        }
    }

    IEnumerator FinishAnim()
    {
        if(isFinished)
        {
        //    if (hudOrderGrid.transform.position.y < AnimStart.transform.position.y)
        //    {
        //        hudOrderGrid.transform.Translate(new Vector3(0, AnimStart.transform.position.y) * Time.deltaTime * 100f, Space.World);
        //        orderTextImage.transform.Translate(new Vector3(0, AnimStart.transform.position.x) * Time.deltaTime * 100f, Space.World);
        //        yield return new WaitForSeconds(0.03f);
        //        StartCoroutine(FinishAnim());
        //    }
        //    else
        //        canChangeScene = true;
        //}
        //else
        //{
        //        if (hudOrderGrid.transform.position.x > AnimEnd.transform.position.x)
        //        {
        //            hudOrderGrid.transform.Translate(new Vector3(AnimEnd.transform.position.x, 0) * Time.deltaTime * 100f, Space.World);
        //            orderTextImage.transform.Translate(new Vector3(AnimEnd.transform.position.x, 0) * Time.deltaTime * 100f, Space.World);
        //            yield return new WaitForSeconds(0.03f);
        //            StartCoroutine(HudMove());
        //        }
        //        else
        //        yield break;
                    
        }
        if (canChangeScene)
        { 
            if (SceneManage.currentSceneIndex < 8)
            {
                SceneManage.GoToNextScene();
            }
            else
            {
                if (Bank.instance.GetaActualMoney() >= Bank.instance.GetGoalNumber())
                    SceneManage.GoToScene("Win");
                else
                    SceneManage.GoToScene("Lose");
            }
        }
        yield break;
    }
}
