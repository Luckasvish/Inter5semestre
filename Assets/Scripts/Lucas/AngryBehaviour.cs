using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBehaviour : MonoBehaviour
{
    public static AngryBehaviour instance;
    void Awake()
    {
        instance = this;
    }

    [SerializeField]
    int waitingTimeToExit;
    [SerializeField]
    int orderingTimeToExit;
    [SerializeField]
    int eatingTimeToExit;

    public int changeWaitingTimeToExit()
    {
        return waitingTimeToExit;
    }

    public int changeOrderingTimeToExit()
    {
        return orderingTimeToExit;
    }

    public int changeEatingTimeToExit()
    {
        return eatingTimeToExit;
    }
}
