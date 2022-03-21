using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itens : MonoBehaviour
{
    Vector3 currentPosition;
    
    internal Chef chef; 

    void Awake()
    {

        chef = FindObjectOfType<Chef>();


    }

    void Update()
    {
        if (Chef.ItenInHand == this)
        {
            transform.position = chef.chefsHands.position;
        }
    }

    public void SetPosition(Vector3 position)
    {
        
        transform.position = position;

    }
}
