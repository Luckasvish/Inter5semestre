using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpatientBehaviour : MonoBehaviour
{
    public static ImpatientBehaviour instance;
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
