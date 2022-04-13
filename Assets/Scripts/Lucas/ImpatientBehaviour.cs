using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpatientBehaviour : MonoBehaviour
{
    [SerializeField]
    int timeToExit;
    public int changeTimeToExit()
    {
        return timeToExit;
    }
}
