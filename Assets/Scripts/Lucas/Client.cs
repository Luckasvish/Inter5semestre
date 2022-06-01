using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class Client : IBehaviour
{
    Order order;
    Chair thisChair;
    [Header("Anger Controller")]
    [SerializeField]
    AngerManager angerManager;


    [Header("Navmesh Atributes")]
    [SerializeField]
    NavMeshAgent navMesh;

    [Header("Money To Pay")]
    [SerializeField]
    int moneyForRecipe;
    [SerializeField]
    int moneyForTip;

    [Header("Irritation Atributes")]
    [SerializeField]
    int increaseAngerValue;
    [SerializeField]
    int timeToGetOut;
    [SerializeField]
    int clientStarterIrritation;


    [Header("UI Food")]
    [SerializeField]
    GameObject InteractionBaloon;    
    [SerializeField]
    GameObject OrderUI;
    Image _OrderUI_Sprite;

    [SerializeField]
    GameObject InteractionImage;


    [Header("UI Irritation Feedback")]
    [SerializeField]
    GameObject[] IrritationImage = new GameObject[3];
    [SerializeField]
    Image IrritationFeedback;
    [SerializeField]
    GameObject IrritationAnimEnd;    

    [Header("UI Pay Feedback")]
    [SerializeField]
    GameObject PayHolder;    
    [SerializeField]
    GameObject[] PayImage = new GameObject[2];
    [SerializeField]
    Image PayFeedback;
    [SerializeField]
    TextMeshProUGUI payValue;
    [SerializeField]
    GameObject PayAnimEnd;


    GameObject myChair;

    [Header("Start/End position")]
    [SerializeField]
    GameObject WayOut;


    [Header("Speech Atributes")]
    [SerializeField]
    SpeechManager[] speech;
    string clientSpeech;
    TextMeshPro interactionText;
    // Index 0 = Story Telling
    // Index 1 = Waiting For Order
    // Index 2 = Order 
    // Index 3 = Waiting Food
    // Index 4 = Eating
    // Index 5 = Paying
    // Index 6 = Paying Tip
    // Index 7 = Getting Out

    bool isGettingOut;
    bool canRage;

    bool hasAvaiableChair;
    bool hasOrdered;
    bool hasFood;
    bool canEat;
    bool hasAte;

    bool isInteractingWithPlayer;

    int maxWaitingTime;
    int maxOrderingTime;
    int maxEatingTime;
    int actualWaitingTime = 0;

    bool callback;
    [Header("Behaviour Atributes")]
    [SerializeField]
    BehaviourState behaviourState;
    public BehaviourType type;

    [Header("Food Atributes")]
    [SerializeField]
    Food foodRef;
    [SerializeField]
    Food[] possibleFood = new Food[3];
    internal string clientOrder;
    internal int clientOrderIndex;

    Animator animator;

    private void Start()
    {
        behaviourState = BehaviourState.WaitingForChair;
        WayOut = SpawnManager.instance.Spawn;
        _OrderUI_Sprite = OrderUI.GetComponent<Image>();
        animator = GetComponentInChildren<Animator>();
        UpdateBehaviour();
        
        WaitingForChair();
    }

    void UpdateBehaviour()
    {
        switch (type)
        {
            case BehaviourType.Calm:
                maxWaitingTime = CalmBehaviour.instance.changeWaitingTimeToExit();
                maxOrderingTime = CalmBehaviour.instance.changeOrderingTimeToExit();
                maxEatingTime = CalmBehaviour.instance.changeEatingTimeToExit();
                StartCoroutine(IrritationFeedbackAnim(0, true));
                break;
            case BehaviourType.Impatient:
                maxWaitingTime = ImpatientBehaviour.instance.changeWaitingTimeToExit();
                maxOrderingTime = ImpatientBehaviour.instance.changeOrderingTimeToExit();
                maxEatingTime = ImpatientBehaviour.instance.changeEatingTimeToExit();
                StartCoroutine(IrritationFeedbackAnim(1, true));
                break;
            case BehaviourType.Angry:
                maxWaitingTime = AngryBehaviour.instance.changeWaitingTimeToExit();
                maxOrderingTime = AngryBehaviour.instance.changeOrderingTimeToExit();
                maxEatingTime = AngryBehaviour.instance.changeEatingTimeToExit();
                StartCoroutine(IrritationFeedbackAnim(2, true));
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
                            StartCoroutine(PayTip());
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

    #region Behaviour
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
        //Debug.Log("Wallking");///
       
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
        //Debug.Log("Walk");
        behaviourState = BehaviourState.Walk;
        Vector3 chairPos = myChair.transform.position;
        navMesh.updatePosition = true;
        navMesh.SetDestination(chairPos);
        if (transform.position != navMesh.destination)
        {
            yield return new WaitForSeconds(1);
           // Debug.Log("AIIIIIIIIIIIII");
           // Debug.Log("volta a andar");
            StartCoroutine(Walk());
            yield break;
        }
            callback = true;
            StartCoroutine(Main());

    //    Debug.Log("passa pro sit");
    }

    public override void Sit()
    {
        behaviourState = BehaviourState.Sit;
        animator.SetInteger("Stage", 1);
        navMesh.updatePosition = false;
        navMesh.ResetPath();
        transform.position = thisChair.clientPosition.transform.position;
        transform.forward = CheckDirectionToSit();
        //play animation
       // Debug.Log("sit");
        callback = true;
        StartCoroutine(Main());
    }

    Vector3 CheckDirectionToSit()
    {
        if (thisChair == thisChair.thisTable.places[1])
        {
   
            return -thisChair.transform.forward;
        }
        else 
        {

        return thisChair.transform.forward;
        }
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

        //randomizeOrder
        int thisClientRecipe = UnityEngine.Random.Range(0, possibleFood.Length);

        //order for client
        clientOrder = possibleFood[thisClientRecipe].foodName;

        //setting order to client chair
        thisChair.GetOrder(clientOrder);

        //setting client order index
        foodRef = OrderManager.instance.AddRecipeToList(possibleFood[thisClientRecipe], this);

        callback = true;
        StartCoroutine(Main());
    }

    public override IEnumerator WaitingFood()
    {
       // Debug.Log("WaitingFood");
        behaviourState = BehaviourState.WaitingFood;
        InteractionBaloon.SetActive(true);
        OrderUI.SetActive(true);

        _OrderUI_Sprite.sprite = OrderManager.instance.GetOrderImage(clientOrder);

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

        hasFood = thisChair.thisTable.CheckIfHasFood(thisChair);

        if (hasFood)
        {
            canEat = thisChair.thisTable.CheckIfClientCanEat(clientOrder,thisChair);
         
            if (canEat)
            {
                callback = true;
                StartCoroutine(Main());
                actualWaitingTime = 0;
            }
            else 
                StartCoroutine(WaitingFood());
        }

        else StartCoroutine(WaitingFood());

    }

    public override IEnumerator Eat()
    {
        behaviourState = BehaviourState.Eat;
        animator.SetInteger("Stage", 2);
        OrderUI.SetActive(false);
        InteractionBaloon.SetActive(false);
       // Debug.Log("Eat");
        hasAte = true;
        if(foodRef != null)
        {

             OrderManager.instance.RemoveRecipeInList(foodRef);
        }

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
        moneyForRecipe = GetMoneyForRecipe();
        int irritation = angerManager.GetIrritation();

        if(type == BehaviourType.Angry && irritation > 25)
        {
            Bank.instance.ChangeMoneyAmount(moneyForRecipe, false);
            yield return new WaitForSeconds(2);
            callback = false;
        }
        else
        {
            Bank.instance.ChangeMoneyAmount(moneyForRecipe, true);
            StartCoroutine(PayFeedbackAnim(0, true, moneyForRecipe));
            yield return new WaitForSeconds(2);
            callback = true;
        }
        StartCoroutine(Main());
    }

    public override IEnumerator PayTip()
    {
        // Debug.Log("PayTip");///
        behaviourState = BehaviourState.PayTip;

        moneyForTip = GetMoneyForTip();

        bool canPay = CheckIfCanPaytip();


        if (canPay)
        {
            Bank.instance.ChangeMoneyAmount(moneyForTip, true);
            StartCoroutine(PayFeedbackAnim(1, true, moneyForTip));
            yield return new WaitForSeconds(2);
            callback = true;

        }
        else 
            callback = false;

        StartCoroutine(Main());
    }

    public override void StartExit()
    {
        behaviourState = BehaviourState.StartExit;
        animator.SetInteger("Stage", 0);
        InteractionImage.SetActive(false);
        OrderUI.SetActive(false);
        InteractionBaloon.SetActive(false);
        

        if(hasOrdered && !hasAte)  OrderManager.instance.RemoveRecipeInList(foodRef);
        else if (hasOrdered && hasAte)
        {
            thisChair.thisTable.CleanClientPlate(thisChair);
        }

        navMesh.updatePosition = true;
        
        navMesh.SetDestination(WayOut.transform.position);
        
        if(myChair != null){ChairManager.instance.AddChair(myChair);}
        
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
            thisChair.ClientGetOff();
            Debug.Log("sem cliente");
        }
        SpawnManager.instance.ChangeClientsNumber(-1);
        Debug.Log("callback");
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
    #endregion

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


    bool CheckIfCanPaytip()
    {
        int payChance = UnityEngine.Random.RandomRange(1, 10);

        if (type == BehaviourType.Calm && payChance > 6)
            return true;
        else if (type == BehaviourType.Impatient && payChance > 8)
            return true;
        else
            return false;
    }

    int GetMoneyForRecipe()
    {
        int value = 10;
        switch(clientOrder)
        {
            case "Feijoada":
                value = 12;
                break;            
            case "PratoFeito":
                value = 10;
                break;            
            case "Buchada":
                value = 15;
                break;
        }

        return value;
    }    
    
    int GetMoneyForTip()
    {
        int value = 0;
        int irritation = angerManager.GetIrritation();
        switch (type)
        {
            case BehaviourType.Calm:
                value = UnityEngine.Random.RandomRange(4,8);
                break;            
            case BehaviourType.Impatient:
                if(irritation > 15)
                    value = UnityEngine.Random.RandomRange(1, 3);
                else if(irritation <= 15)
                    value = UnityEngine.Random.RandomRange(3, 5);
                break;            
        }

        return value;
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

    public BehaviourState GetActualBehaviour()
    {
        return behaviourState;
    }    

    public void ChangeIrritation()
    {
        switch (type)
        {
            case BehaviourType.Calm:
                type = BehaviourType.Impatient;  
                break;
            case BehaviourType.Impatient:
                type = BehaviourType.Angry;
                break;
        }
    }

    public int SetStarterIrritation()
    {
        int irritationNumber = UnityEngine.Random.RandomRange(1,3);
        switch (irritationNumber)
        {
            case 1:
                type = BehaviourType.Calm;
                increaseAngerValue = 1;
                break;
            case 2:
                type = BehaviourType.Impatient;
                increaseAngerValue = 4;
                break;            
            case 3:
                type = BehaviourType.Angry;
                increaseAngerValue = 2;
                break;
        }
        return increaseAngerValue;
    }

    public int RandomizeAngerBar()
    {
        bool moreAnger = false;
        int angerValue = UnityEngine.Random.RandomRange(0, 10);

        if (angerValue > 8)
            moreAnger = true;

        switch (type)
        {
            case BehaviourType.Calm:
                if (moreAnger)
                    clientStarterIrritation = UnityEngine.Random.RandomRange(16, 20);
                else
                    clientStarterIrritation = UnityEngine.Random.RandomRange(0, 16);
                break;
            case BehaviourType.Impatient:
                if (moreAnger)
                    clientStarterIrritation = UnityEngine.Random.RandomRange(10, 20);
                else
                    clientStarterIrritation = UnityEngine.Random.RandomRange(0, 10);
                break;
            case BehaviourType.Angry:
                if (moreAnger)
                    clientStarterIrritation = UnityEngine.Random.RandomRange(8, 20);
                else
                    clientStarterIrritation = UnityEngine.Random.RandomRange(0, 8);
                break;
        }
        return clientStarterIrritation;
    }

    #region Feedbacks
    IEnumerator IrritationFeedbackAnim(int spriteIndex, bool reset_pos)
    {
        if (IrritationFeedback.gameObject.transform.position.y >= IrritationAnimEnd.transform.position.y)
        {
            IrritationFeedback.gameObject.SetActive(false);
            yield break;
        }
        if (reset_pos)
        {
            IrritationFeedback.color = new Color(255,255,255,0);
            IrritationFeedback.gameObject.transform.position = gameObject.transform.position;
            IrritationFeedback.gameObject.SetActive(true);
            yield return new WaitForSeconds(.1f);
            IrritationFeedback.color = new Color(255, 255, 255, 1);
        }

        IrritationFeedback.sprite = IrritationImage[spriteIndex].GetComponent<Image>().sprite;
        if (IrritationFeedback.gameObject.transform.position.y < IrritationAnimEnd.transform.position.y)
        {
            IrritationFeedback.gameObject.transform.Translate(new Vector3 (0, IrritationAnimEnd.transform.position.y) * Time.deltaTime * 2f, Space.World);
            yield return new WaitForSeconds(.04f);
            StartCoroutine(IrritationFeedbackAnim(spriteIndex, false));
        }
    }

    IEnumerator PayFeedbackAnim(int spriteIndex, bool reset_pos, int value)
    {
        if (PayHolder.gameObject.transform.position.y >= PayAnimEnd.transform.position.y && !reset_pos)
        {
            PayHolder.gameObject.SetActive(false);
            yield break;
        }
        if (reset_pos)
        {
            PayFeedback.color = new Color(255, 255, 255, 0);
            payValue.text = "";
            PayHolder.gameObject.transform.position = gameObject.transform.position;
            PayHolder.gameObject.SetActive(true);
            payValue.text = "+" + value.ToString();
            yield return new WaitForSeconds(.1f);
            PayFeedback.color = new Color(255, 255, 255, 1);
        }

        PayFeedback.sprite = PayImage[spriteIndex].GetComponent<Image>().sprite;
        if (PayHolder.gameObject.transform.position.y < PayAnimEnd.transform.position.y)
        {
            PayHolder.gameObject.transform.Translate(new Vector3(0, PayAnimEnd.transform.position.y) * Time.deltaTime * 2f, Space.World);
            yield return new WaitForSeconds(.1f);
            StartCoroutine(PayFeedbackAnim(spriteIndex, false, value));
        }
    }
    #endregion

}