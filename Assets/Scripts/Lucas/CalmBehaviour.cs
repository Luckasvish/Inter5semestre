using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalmBehaviour : MonoBehaviour
{
    public static CalmBehaviour instance;

    void Awake()
    {
        instance = this;
    }

    [SerializeField]
    int timeToExit;
    public int changeTimeToExit()
    {
        return timeToExit;
    }

}
