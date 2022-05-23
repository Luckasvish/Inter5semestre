using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bank : MonoBehaviour
{
    static public Bank instance;
    [SerializeField]
    private int startMoney;

    [SerializeField]
    TextMeshProUGUI moneyInUI;

    [SerializeField]
    TextMeshProUGUI goal;
    [SerializeField]
    int goalNumber = 800;

    int actualMoney;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        goal.text = goalNumber.ToString();
        UpdateMoneyUI();
    }

    public int GetGoalNumber()
    {
        return goalNumber;
    }
    
    public int GetaActualMoney()
    {
        return actualMoney;
    }

    public void ChangeMoneyAmount(int money, bool isIncreasing)
    {
        //increase the money and then uptade the money UI
        if(isIncreasing)
            actualMoney += money;
        else
            actualMoney -= money;
        UpdateMoneyUI();
    }    

    public void UpdateMoneyUI()
    {
        //change the money UI text for the actual value
        moneyInUI.text = actualMoney.ToString();
    }
}
