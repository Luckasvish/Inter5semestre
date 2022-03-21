using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balcon : MonoBehaviour
{
    
    public Transform targetPosition;
    internal bool hasItem;

    internal Itens iten;
    public Vector3 SetItemPosition()
    {   
        return targetPosition.position;
    }

}
