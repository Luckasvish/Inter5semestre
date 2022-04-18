using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Client : MonoBehaviour, IBehaviour
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

    bool hasOrdered;
    bool hasFood;
    bool canEat;

    bool isInteractingWithPlayer;
    string[] possibleRecipe = new string[3];
    string clientOrder;


    [SerializeField]
    CalmBehaviour calm;
    [SerializeField]
    ImpatientBehaviour impatient;
    [SerializeField]
    AngryBehaviour angry;
    [SerializeField]
    RageBehaviour rage;

    int maxWaitingTime;
    int maxOrderingTime;
    int maxEatingTime;


    public Action<bool> callback;
    IBehaviour.BehaviourState behaviourState;
    IBehaviour.BehaviourType type;
    private void Start()
    {
        type = IBehaviour.BehaviourType.Calm;
        UpdateBehaviour();
        CheckPossibleRecipes();
        Walk(callback);
        Debug.Log(this);
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
            case IBehaviour.BehaviourType.Calm:
                maxWaitingTime = CalmBehaviour.instance.changeWaitingTimeToExit();
                maxOrderingTime = CalmBehaviour.instance.changeOrderingTimeToExit();
                maxEatingTime = CalmBehaviour.instance.changeEatingTimeToExit();
                break;
            case IBehaviour.BehaviourType.Impatient:
                maxWaitingTime = ImpatientBehaviour.instance.changeWaitingTimeToExit();
                maxOrderingTime = ImpatientBehaviour.instance.changeOrderingTimeToExit();
                maxEatingTime = ImpatientBehaviour.instance.changeEatingTimeToExit();
                break;
            case IBehaviour.BehaviourType.Angry:
                maxWaitingTime = AngryBehaviour.instance.changeWaitingTimeToExit();
                maxOrderingTime = AngryBehaviour.instance.changeOrderingTimeToExit();
                maxEatingTime = AngryBehaviour.instance.changeEatingTimeToExit();
                break;
        }
    }

    void Main()
    {
        Walk(x => 
        {
            UpdateBehaviour();
            switch (x) 
            { 
                case true: Sit(callback); 
                    break; 
                case false:
                    StartExit(callback); 
                    break; 
            }
        });
        Sit(x =>
        {
            UpdateBehaviour();
            switch (x)
            {
                case true:
                    WaitingForOrder(callback);
                    break;
                case false:
                    StartExit(callback);
                    break;
            }
        });
        WaitingForOrder(x => 
        {
            UpdateBehaviour();
            switch (x) 
            { 
                case true:
                    Order(callback);
                    break; 
                case false:
                    StartExit(callback);
                    break; 
            }
        });
        Order(x => 
        {
            UpdateBehaviour();
            switch (x) 
            { 
                case true: 
                    StartCoroutine(WaitingFood(callback)); 
                    break; 
                case false:
                    StartExit(callback);
                    break; 
            }
        });
        WaitingFood(x => 
        {
            UpdateBehaviour();
            switch (x)
            {
                case true:
                    Eat(callback);
                    break;
                case false:
                    StartExit(callback);
                    break;
            }
        });
        Eat(x =>
        {
            UpdateBehaviour();
            switch (x)
            {
                case true:
                    StartCoroutine(PayOrder(callback));
                    break;
                case false:
                    StartExit(callback);
                    break;
            }
        });
        PayOrder(x =>
        {
            UpdateBehaviour();
            switch (x)
            {
                case true:
                    PayTip(callback);
                    break;
                case false:
                    StartExit(callback);
                    break;
            }
        });
        PayTip(x =>
        {
            UpdateBehaviour();
            switch (x)
            {
                case true:
                    StartExit(callback);
                    break;
                case false:
                    StartExit(callback);
                    break;
            }
        });
        StartExit(x =>
        {
            UpdateBehaviour();
            switch (x)
            {
                case true:
                    StartCoroutine(EndExit(callback));
                    break;
                case false:
                    Rage(callback);
                    break;
            }
        });
        Rage(x =>
        {
            UpdateBehaviour();
            switch (x)
            {
                case true:
                    StartCoroutine(EndExit(callback));
                    break;
                case false:
                    StartCoroutine(EndExit(callback));
                    break;
            }
        });
        EndExit(x =>
        {
            UpdateBehaviour();
            switch (x)
            {
                case true:
                    Destroy(gameObject);
                    break;
            }
        });
    }
    //public IEnumerator BehaviourManager()
    //{

    //    yield return new WaitForSeconds(timeToUpdateBehaviour);
    //    StartCoroutine(BehaviourManager());
    //}

    public void Walk(Action<bool> callback)
    {
        myChair = ChairManager.instance.GetChair();
        thisChair = myChair.GetComponent<Chair>();
        Vector3 chairPos = myChair.transform.position;
        transform.position = chairPos;
        //transform.Translate(chairPos, Space.World);
        callback(true);
    }

    public void Sit(Action<bool> callback)
    {
        //play animation
        callback(true);
    }

    public IEnumerator WaitingForOrder(Action<bool> callback)
    {
        this.callback = callback;
        int actualWaitingTime = 0;
        if (actualWaitingTime == maxWaitingTime)
        {
            callback(false);
            yield break;
        }
        else
        {
            actualWaitingTime += 1;
            yield return new WaitForSeconds(1);
        }

        if (hasOrdered)
            callback(true);
        else
            StartCoroutine(WaitingFood(this.callback));
    }

    public void Order(Action<bool> callback)
    {
        int thisClientRecipe = UnityEngine.Random.Range(0, possibleRecipe.Length);
        clientOrder = possibleRecipe[thisClientRecipe];

        order = new Order();
        order.GetRecipe(clientOrder);
        thisChair.GetOrder(clientOrder);
        OrderManager.instance.AddRecipeToList(clientOrder);
        callback(true);
    }

    public IEnumerator WaitingFood(Action<bool> callback)
    {
        this.callback = callback;
        int actualWaitingTime = 0;
        if(actualWaitingTime == maxWaitingTime)
        {
            callback(false);
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
                callback(true);
            else 
                StartCoroutine(WaitingFood(this.callback));
        }
        else
            StartCoroutine(WaitingFood(this.callback));
    }

    public IEnumerator Eat(Action<bool> callback)
    {
        OrderManager.instance.RemoveRecipeInList(clientOrder);
        int actualWaitingTime = 0;
        if(actualWaitingTime == maxEatingTime)
        callback(true);
        yield break;
    }    
    

    public IEnumerator PayOrder(Action<bool> callback)
    {
        UpdateBehaviour();
        Bank.instance.EarnMoney(moneyToGet);
        callback(true);
        yield break;
    }

    public IEnumerator PayTip(Action<bool> callback)
    {
        callback(true);
        yield break;
    }

    public void StartExit(Action<bool> callback)
    {
        ChairManager.instance.AddChair(myChair);
        callback(true);
    }    

    public IEnumerator EndExit(Action<bool> callback)
    {
        if(transform.position == WayOut.transform.position)
        {
            callback(true); 
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(1);
            transform.position = WayOut.transform.position;
        }
    }

    public IEnumerator InteractWithClients(Action<bool> callback)
    {
        callback(true);
        yield break;
    }

    public void Rage(Action<bool> callback)
    {
        callback(false);
    }
}
