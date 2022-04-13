using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Client : MonoBehaviour, IBehaviour
{

    int moneyToGet;
    
    [SerializeField]
    int timeToGetOut;
    bool hasFood;
    bool isInteractingWithPlayer;
    string[] possibleRecipe = new string[3];
    

    [SerializeField]
    CalmBehaviour calm;
    [SerializeField]
    ImpatientBehaviour impatient;
    [SerializeField]
    AngryBehaviour angry;
    [SerializeField]
    RageBehaviour rage;

    int maxWaitingTime;
    int maxEatingTime;


    public Action<bool> callback;
    IBehaviour.BehaviourState behaviourState;
    IBehaviour.BehaviourType type;
    private void Start()
    {
        UpdateBehaviour();
        CheckPossibleRecipes();
        Walk(callback);
    }
    void CheckPossibleRecipes()
    {
        for (int i = 0; i < possibleRecipe.Length; i++)
        {
            possibleRecipe[i] = "";
        }
    }
    void UpdateBehaviour()
    {
        switch (type)
        {
            case IBehaviour.BehaviourType.Calm:
                maxWaitingTime = calm.changeTimeToExit();
                break;
            case IBehaviour.BehaviourType.Impatient:
                maxWaitingTime = impatient.changeTimeToExit();
                break;
            case IBehaviour.BehaviourType.Angry:
                maxWaitingTime = angry.changeTimeToExit();
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
                    Rage(callback);
                    break;
                case false:
                    StartCoroutine(EndExit(callback));
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
        Vector3 chairPos = ChairManager.instance.ChooseChairToGetPosition();
        transform.Translate(chairPos, Space.World);
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

        if (isInteractingWithPlayer)
            callback(true);
        else
            StartCoroutine(WaitingFood(this.callback));
        yield break;
    }

    public void Order(Action<bool> callback)
    {
        int thisClientRecipe = UnityEngine.Random.Range(0, possibleRecipe.Length);

        Order order = new Order();
        for (int i = 0; i < possibleRecipe.Length; i++)
        {
            if(i == thisClientRecipe)
            {
                order.GetRecipe(possibleRecipe[i]);
                break;
            }
        }
        maxWaitingTime = timeToGetOut;
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
        Chair.instance.CheckIfHasFood();
        if (hasFood)
            callback(true);
        else
            StartCoroutine(WaitingFood(this.callback));
    }

    public IEnumerator Eat(Action<bool> callback)
    {
        int actualWaitingTime = 0;
        UpdateBehaviour();
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
        callback(true);
    }    

    public IEnumerator EndExit(Action<bool> callback)
    {
        callback(true);
        yield break;
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
