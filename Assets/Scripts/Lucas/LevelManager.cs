using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    float alpha = 0;

    [SerializeField]
    GameObject DayIndicator;    
    [SerializeField]
    GameObject AnimStart;    
    [SerializeField]
    GameObject AnimEnd;    
    [SerializeField]
    GameObject BgAnim;        
    [SerializeField]
    GameObject AnimatedObject;    
    [SerializeField]
    GameObject[] SpriteAnim = new GameObject[2];

    [SerializeField]
    TextMeshProUGUI timerMinutesText;    
    [SerializeField]
    TextMeshProUGUI timerSecondsText;
    [SerializeField]
    TextMeshProUGUI faseText;

    public static bool isFinished = false;
    bool canChangeScene = false;
    bool firstTime = true;


    // Start is called before the first frame update
    void Start()
    {
        faseText.text = SceneManage.SceneName();
        AnimatedObject.transform.position = AnimStart.transform.position;
        DayIndicator.GetComponent<Image>().sprite = SpriteAnim[0].GetComponent<Image>().sprite;
        isFinished = false;
        canChangeScene = false;
        actualTimerNumber = startTimerNumber;
        timerMinutesText.text = "0" + (((actualTimerNumber / 2) / 30) % 24).ToString() + ": ";
        timerSecondsText.text = " 0" + (actualTimerNumber % 30).ToString();
        StartCoroutine(Timer());
        StartCoroutine(FinishAnim());
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

        if (actualTimerNumber >= endTimerNumber - 20)
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
            DayIndicator.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            DayIndicator.GetComponent<Image>().sprite = SpriteAnim[1].GetComponent<Image>().sprite;
            StartCoroutine(FinishAnim());
        }
    }
    int debug;
    IEnumerator FinishAnim()
    {
        if(!firstTime)
        {
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

            if (!isFinished)
            {
                if (AnimatedObject.transform.position.y < AnimEnd.transform.position.y)
                {
                    BgAnim.GetComponent<Image>().color = new Color(65, 65, 65, 0);
                    AnimatedObject.GetComponent<Image>().sprite = SpriteAnim[0].GetComponent<Image>().sprite;
                    AnimatedObject.transform.Translate(new Vector3(0, AnimEnd.transform.position.y) * Time.deltaTime * 4f, Space.World);
                    yield return new WaitForSeconds(0.03f);
                    StartCoroutine(FinishAnim());
                }
                else
                {
                    AnimatedObject.transform.position = AnimStart.transform.position;
                    yield break;
                }

            }
            else
            {
                if (AnimatedObject.transform.position.y < AnimEnd.transform.position.y )
                {
                    debug++;
                    Debug.Log(debug);
                    AnimatedObject.GetComponent<Image>().sprite = SpriteAnim[1].GetComponent<Image>().sprite;
                    AnimatedObject.transform.Translate(new Vector3(0, AnimEnd.transform.position.y) * Time.deltaTime * 6f, Space.World);
                    if(alpha < 0.7f)
                        alpha+= 0.1f;
                    BgAnim.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
                    yield return new WaitForSeconds(0.03f);
                    StartCoroutine(FinishAnim());
                }
                else
                {
                    canChangeScene = true;
                    yield return new WaitForSeconds(3f);
                    StartCoroutine(FinishAnim());
                }

            }
        }
        else
        {
            yield return new WaitForSeconds(2f);
            firstTime = false;
            StartCoroutine(FinishAnim());
        }

    }
}
