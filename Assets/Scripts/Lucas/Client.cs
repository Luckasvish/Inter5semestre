using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class Client : IBehaviour
{
    [Header("Navmesh Atributes")]
    [SerializeField]
    NavMeshAgent navMesh;

    [SerializeField]
    Collider collider;


    [SerializeField]
    int moneyToGet = 10;


    [Header("UI")]
    [SerializeField]
    GameObject InteractionBaloon;    
    [SerializeField]
    GameObject OrderUI;
    Image _OrderUI_Sprite;
    [SerializeField]
    Image[] OrderImages = new Image[3];
    [SerializeField]
    GameObject InteractionImage;

    GameObject myChair;
    Order order;
    Chair thisChair;

    [SerializeField]
    GameObject WayOut;

    [SerializeField]
    int timeToGetOut;

    [Header("Speech")]
    string clientSpeech;
    [SerializeField]
    SpeechManager[] speech;
    // Index 0 = Story Telling
    // Index 1 = Waiting For Order
    // Index 2 = Order 
    // Index 3 = Waiting Food
    // Index 4 = Eating
    // Index 5 = Paying
    // Index 6 = Paying Tip
    // Index 7 = Getting Out

    bool isGettingOut;

    bool hasAvaiableChair;
    bool hasOrdered;
    bool hasFood;
    bool canEat;

    bool isInteractingWithPlayer;
    string[] possibleRecipe = new string[3];
    internal string clientOrder;

    int maxWaitingTime;
    int maxOrderingTime;
    int maxEatingTime;
    int actualWaitingTime = 0;

    bool callback;
    [SerializeField]
    BehaviourState behaviourState;
    BehaviourType type;


    TextMeshPro interactionText;



    private void Start()
    {
        WayOut = SpawnManager.instance.Spawn;
        _OrderUI_Sprite = OrderUI.GetComponent<Image>();
        type = BehaviourType.Calm;
        UpdateBehaviour();
        
        CheckPossibleRecipes();
        WaitingForChair();
    }

    private void Update()
    {
    }
    void CheckPossibleRecipes()
    {
        for (int i = 0; i < possibleRecipe.Length; i++)
        {
            possibleRecipe[i] = MacroSistema.sistema.GetPossibleRecipes(i);
        }
    }

    void UpdateBehaviour()
    {
        switch (type)
        {
            case BehaviourType.Calm:
                maxWaitingTime = CalmBehaviour.instance.changeWaitingTimeToExit();
                maxOrderingTime = CalmBehaviour.instance.changeOrderingTimeToExit();
                maxEatingTime = CalmBehaviour.instance.changeEatingTimeToExit();
                break;
            case BehaviourType.Impatient:
                maxWaitingTime = ImpatientBehaviour.instance.changeWaitingTimeToExit();
                maxOrderingTime = ImpatientBehaviour.instance.changeOrderingTimeToExit();
                maxEatingTime = ImpatientBehaviour.instance.changeEatingTimeToExit();
                break;
            case BehaviourType.Angry:
                maxWaitingTime = AngryBehaviour.instance.changeWaitingTimeToExit();
                maxOrderingTime = AngryBehaviour.instance.changeOrderingTimeToExit();
                maxEatingTime = AngryBehaviour.instance.changeEatingTimeToExit();
                break;
        }
    }

    IEnumerator Main()
    {
        yield return new WaitForSeconds(0.1f);
        UpdateBehaviour();
        InteractWithClients();
        switch (behaviourState)
        {
            case BehaviourState.WaitingForChair:
                switch (callback)
                {
                    case true:
                        StartCoroutine(Walk());
                        break;
                    case false:
                        StartExit();
                        break;
                }
                break;
            case BehaviourState.Walk:
                    switch (callback)
                    {
                        case true:
                            Sit();
                            break;
                        case false:
                            StartExit();
                            break;
                    }
                break;
            case BehaviourState.Sit:
                    switch (callback)
                    {
                        case true:
                            StartCoroutine(WaitingForOrder());
                            break;
                        case false:
                            StartExit();
                            break;
                    }
                break;
            case BehaviourState.WaitingForOrder:
                    switch (callback)
                    {
                        case true:
                            Order();
                            break;
                        case false:
                            StartExit();
                            break;
                    }
                break;
            case BehaviourState.Order:
                    switch (callback)
                    {
                        case true:
                            StartCoroutine(WaitingFood());
                            break;
                        case false:
                            StartExit();
                            break;
                    }
                break;
            case BehaviourState.WaitingFood:
                    switch (callback)
                    {
                        case true:
                            StartCoroutine(Eat());
                            break;
                        case false:
                            StartExit();
                            break;
                    }
                break;
            case BehaviourState.Eat:
                    switch (callback)
                    {
                        case true:
                            StartCoroutine(PayOrder());
                            break;
                        case false:
                            StartExit();
                            break;
                    }
                break;
            case BehaviourState.PayOrder:
                    switch (callback)
                    {
                        case true:
                            PayTip();
                            break;
                        case false:
                            StartExit();
                            break;
                    }
                break;
            case BehaviourState.PayTip:
                    switch (callback)
                    {
                        case true:
                            StartExit();
                            break;
                        case false:
                            StartExit();
                            break;
                    }
                break;
            case BehaviourState.StartExit:
                    switch (callback)
                    {
                        case true:
                            StartCoroutine(EndExit());
                            break;
                        case false:
                            Rage();
                            break;
                    }
                break;
            case BehaviourState.Rage:
                    switch (callback)
                    {
                        case true:
                            StartCoroutine(EndExit());
                            break;
                        case false:
                            StartCoroutine(EndExit());
                            break;
                    }
                break;
            case BehaviourState.EndExit:
                    switch (callback)
                    {
                        case true:
                            Destroy(gameObject);
                            break;
                    }
                break;
        }

    }

    public override void WaitingForChair()
    {
        if (SpawnManager.instance.GetClientsNumber() == SpawnManager.instance.GetMaxClientPerLevel() -2)
        {
            int valueToGetOut = UnityEngine.Random.RandomRange(0,10);
            if(valueToGetOut <= 2)
            {
                callback = false;
                isGettingOut = true;
                StartCoroutine(Main());
            }
        }

        behaviourState = BehaviourState.WaitingForChair;
        hasAvaiableChair = ChairManager.instance.CheckIfHasAvaiableChair();
          Debug.Log("Wallking");///
       
        if (hasAvaiableChair)
        {   
            myChair = ChairManager.instance.GetChair();
            thisChair = myChair.GetComponent<Chair>();
            thisChair.client = this;
            callback = true;
            StartCoroutine(Main());
        }
        else
        {
            callback = false;
            StartCoroutine(Main());
        }
    }
    public override IEnumerator Walk()
    {
        Debug.Log("Walk");
        behaviourState = BehaviourState.Walk;
        yield return new WaitForSeconds(2);
        Vector3 chairPos = myChair.transform.position;
        this.navMesh.destination = chairPos;
        if (transform.position != navMesh.destination)
        {
            Debug.Log("volta a andar");
            StartCoroutine(Walk());
            yield break;
        }

        Debug.Log("passa pro sit");
        callback = true;
        StartCoroutine(Main());
    }

    public override void Sit()
    {
        behaviourState = BehaviourState.Sit;
        //play animation
        Debug.Log("sit");
        callback = true;
        StartCoroutine(Main());
    }

    public override IEnumerator WaitingForOrder()
    {
        behaviourState = BehaviourState.WaitingForOrder;
        InteractionImage.SetActive(true);

        if (actualWaitingTime == maxWaitingTime)
        {
            callback = false;
            StartCoroutine(Main());
            yield break;
        }
        else
        {
            actualWaitingTime += 1;
            yield return new WaitForSeconds(1);
        }

        if (hasOrdered)
        { 
            callback = true;
            StartCoroutine(Main());
            actualWaitingTime = 0;
        }
        else
            StartCoroutine(WaitingForOrder());
    }

    public override void Order()
    {
        behaviourState = BehaviourState.Order;
        InteractionImage.SetActive(false);

        int thisClientRecipe = UnityEngine.Random.Range(0, possibleRecipe.Length);
        clientOrder = possibleRecipe[thisClientRecipe];
        Debug.Log(clientOrder);
        order = new Order();
        order.GetRecipe(clientOrder);
        thisChair.GetOrder(clientOrder);
        OrderManager.instance.AddRecipeToList(clientOrder);
        callback = true;
        StartCoroutine(Main());
    }

    public override IEnumerator WaitingFood()
    {
        Debug.Log("WaitingFood");
        behaviourState = BehaviourState.WaitingFood;
        InteractionBaloon.SetActive(true);
        OrderUI.SetActive(true);

        
        switch(clientOrder)
        {
            case "Feijoada":
                _OrderUI_Sprite.sprite = OrderImages[0].sprite;
                break;
            case "PratoFeito":
                _OrderUI_Sprite.sprite = OrderImages[1].sprite;
                break;
            case "Buchada":
                _OrderUI_Sprite.sprite = OrderImages[2].sprite;
                break;
        }


        if(actualWaitingTime == maxWaitingTime)
        {
            callback = false;
            StartCoroutine(Main());
            yield break;
        }
        else
        {
            actualWaitingTime += 1;
            yield return new WaitForSeconds(1);
        }
        hasFood = thisChair.CheckIfHasFood();
        if (hasFood)
        {
            canEat = thisChair.CheckFood();

            if (canEat)
            {
                callback = true;
                StartCoroutine(Main());
                actualWaitingTime = 0;
            }
            else 
                StartCoroutine(WaitingFood());
        }
        else
            StartCoroutine(WaitingFood());
    }

    public override IEnumerator Eat()
    {
        behaviourState = BehaviourState.Eat;
        OrderUI.SetActive(false);
        InteractionBaloon.SetActive(false);
        Debug.Log("Eat");
        OrderManager.instance.RemoveRecipeInList(clientOrder);

        if(actualWaitingTime == maxEatingTime)
        {
            callback = true;
            StartCoroutine(Main());
        }
        else
        {
            actualWaitingTime += 1;
            yield return new WaitForSeconds(1);
            StartCoroutine(Eat());
        }
    }    
    
    
    public override IEnumerator PayOrder()
    {
        behaviourState = BehaviourState.PayOrder;
        Debug.Log("PayOrder");
        UpdateBehaviour();
        Bank.instance.EarnMoney(moneyToGet);
        callback = true;
        StartCoroutine(Main());
        yield return new WaitForSeconds(2);
    }

    public override IEnumerator PayTip()
    {
        Debug.Log("PayTip");///
        behaviourState = BehaviourState.PayTip;
        callback = true;
        StartCoroutine(Main());
        yield break;
    }

    public override void StartExit()
    {
        behaviourState = BehaviourState.StartExit;
        InteractionImage.SetActive(false);
        OrderUI.SetActive(false);
        InteractionBaloon.SetActive(false);
        Debug.Log("Dando o fora!");///
        navMesh.destination = WayOut.transform.position;
        if (myChair != null)
            ChairManager.instance.AddChair(myChair);
        callback = true;
        StartCoroutine(Main());
    }    

    public override IEnumerator EndExit()
    {
        behaviourState = BehaviourState.EndExit;
        if (transform.position != navMesh.destination)
        {
            yield return new WaitForSeconds(1);
            navMesh.destination = WayOut.transform.position;
            StartCoroutine(EndExit());
            yield break;
        }

        Debug.Log("vazei");
        if (thisChair != null)
        {
            thisChair.client = null;
        }
        SpawnManager.instance.ChangeClientsNumber(-1);
        callback = true;
        StartCoroutine(Main());
        yield break;

    }

    public override void InteractWithClients()
    {
        if (!isGettingOut)
        {
            int interactValue = UnityEngine.Random.RandomRange(0, 10);
            if (interactValue > 3)
                return;
        }

        //draw image with text
        //interactionText.text = TextForInteraction();
    }

    public string TextForInteraction()
    {
        /////////////////////Summary
        /// SpeechIndex
        ///
        // Index 0 = Story Telling
        // Index 1 = Waiting For Order
        // Index 2 = Order 
        // Index 3 = Waiting Food
        // Index 4 = Eating
        // Index 5 = Paying
        // Index 6 = Paying Tip
        // Index 7 = Getting Out
        switch (behaviourState)
        {
            case BehaviourState.Walk:
                clientSpeech = speech[0].RandomizeString();
                break;
            case BehaviourState.WaitingForOrder:
                clientSpeech = speech[1].RandomizeString();
                break;
            case BehaviourState.Order:
                clientSpeech = speech[2].RandomizeString();
                break;
            case BehaviourState.WaitingFood:
                clientSpeech = speech[3].RandomizeString();
                break;
            case BehaviourState.Eat:
                clientSpeech = speech[4].RandomizeString();
                break;
            case BehaviourState.PayOrder:
                clientSpeech = speech[5].RandomizeString();
                break;
            case BehaviourState.PayTip:
                clientSpeech = speech[6].RandomizeString();
                break;
            case BehaviourState.StartExit:
                if(isGettingOut)
                    clientSpeech = speech[7].RandomizeString();
                else
                    clientSpeech = speech[0].RandomizeString();
                break;
        }
        return clientSpeech;
    }

    public void Rage()
    {
        behaviourState = BehaviourState.Rage;
        callback = false;
        StartCoroutine(Main());
    }

    public bool GetIfHasOrdered()
    {
        return hasOrdered;
    }

    public void Ordering()
    {
        if(!hasOrdered && behaviourState == BehaviourState.WaitingForOrder)
        hasOrdered = true;
    }

    public int GetImageForClientRecipe()
    {
        for (int i = 0; i < possibleRecipe.Length; i++)
        {
            if (possibleRecipe[i] == clientOrder)
            {
                return i;
            }
        }
        return -1;

    }

    public IBehaviour.BehaviourState GetActualBehaviour()
    {
        return behaviourState;
    }
}
