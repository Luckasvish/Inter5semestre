using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Client : IBehaviour
{
    [SerializeField]
    int moneyToGet;

    GameObject myChair;
    Order order;
    Chair thisChair;

    [SerializeField]
    GameObject WayOut;

    [SerializeField]
    int timeToGetOut;

    public bool hasAvaiableChair;
    internal bool hasOrdered;
    bool hasFood;
    bool canEat;

    bool isInteractingWithPlayer;
    string[] possibleRecipe = new string[3];
    string clientOrder;

    int maxWaitingTime;
    int maxOrderingTime;
    int maxEatingTime;



    int actualWaitingTime = 0;

    public bool callback;
    public BehaviourState behaviourState;
    BehaviourType type;
   
  
  
    private void Start()
    {
        WayOut = SpawnManager.instance.Spawn;
        type = BehaviourType.Calm;
        UpdateBehaviour();
        CheckPossibleRecipes();
        Walk();
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
        switch (behaviourState)
        {
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
                            InteractWithClients();
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
                            Eat();
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
    //public IEnumerator BehaviourManager()
    //{

    //    yield return new WaitForSeconds(timeToUpdateBehaviour);
    //    StartCoroutine(BehaviourManager());
    //}

    public override void Walk()
    {
        hasAvaiableChair = ChairManager.instance.CheckIfHasAvaiableChair();
       
        if (hasAvaiableChair)
        {
            myChair = ChairManager.instance.GetChair();
            behaviourState = BehaviourState.Walk;
            Debug.Log("MyChair : "+ myChair);/////////////////
            thisChair = myChair.GetComponent<Chair>();
            Debug.Log("ThisChair : "+ thisChair);/////////////////
            thisChair.client = this;
            Vector3 chairPos = myChair.transform.position;
            transform.position = chairPos;
            //transform.Translate(chairPos, Space.World);
            callback = true;
            StartCoroutine(Main());
        }
        else
        {
            callback = false;
            StartCoroutine(Main());
        }
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
        Debug.Log("WaitingForOrder");

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
            actualWaitingTime = 0;
        }
        else
            StartCoroutine(WaitingForOrder());
    }

    public override void Order()
    {
        behaviourState = BehaviourState.Order;
        Debug.Log("Order");
        int thisClientRecipe = UnityEngine.Random.Range(0, possibleRecipe.Length);
        clientOrder = possibleRecipe[thisClientRecipe];

        order = new Order();
        order.GetRecipe(clientOrder);
        thisChair.GetOrder(clientOrder);
        OrderManager.instance.AddRecipeToList(clientOrder);
        callback = true;
        StartCoroutine(Main());
    }

    public override IEnumerator WaitingFood()
    {
        behaviourState = BehaviourState.WaitingFood;
        Debug.Log("WaitingFood");
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
        Debug.Log("Eat");
        OrderManager.instance.RemoveRecipeInList(clientOrder);

        if(actualWaitingTime == maxEatingTime)
        {
            callback = true;
            StartCoroutine(Main());
            actualWaitingTime = 0;
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
        yield break;
    }

    public override IEnumerator PayTip()
    {
        behaviourState = BehaviourState.PayTip;
        callback = true;
        StartCoroutine(Main());
        yield break;
    }

    public override void StartExit()
    {
        behaviourState = BehaviourState.StartExit;
        if (myChair != null)
            ChairManager.instance.AddChair(myChair);
        callback = true;
        StartCoroutine(Main());
    }    

    public override IEnumerator EndExit()
    {
        behaviourState = BehaviourState.EndExit;
        if (transform.position == WayOut.transform.position)
        {
            callback = true;
            
            if(thisChair != null)
            {
                thisChair.client = null;
            }

            StartCoroutine(Main());
            
        }
        else
        {
            yield return new WaitForSeconds(1);
            transform.position = WayOut.transform.position;
            StartCoroutine(EndExit());
        }
    }

    public override IEnumerator InteractWithClients()
    {
        callback = true;
        StartCoroutine(Main());
        yield break;
    }

    public void Rage()
    {
        behaviourState = BehaviourState.Rage;
        callback = false;
        StartCoroutine(Main());
    }
}
