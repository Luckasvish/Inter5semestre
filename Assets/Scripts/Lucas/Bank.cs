using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    static public Bank instance;
    [SerializeField]
    private int startMoney;

    [SerializeField]
    TextMeshProUGUI moneyInUI;
    public int yourMoney;
    public string actualMoney;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateMoneyUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void EarnMoney(int earnedMoney)
    {
        //increase the money and then uptade the money UI
        yourMoney += earnedMoney;
        UpdateMoneyUI();
    }    
    
    public void LostMoney(int moneyLoss)
    {
        //decrease the money and then uptade the money UI
        yourMoney -= moneyLoss;
        UpdateMoneyUI();
    }

    public void UpdateMoneyUI()
    {
        //change the money UI text for the actual value
        moneyInUI.text = actualMoney + " = O taldo dinheiro do Taldo Buteco";   
    }
}
