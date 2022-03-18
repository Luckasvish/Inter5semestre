using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itens : MonoBehaviour
{
    Vector3 currentPosition;



    void Update()
    {
        transform.position = currentPosition;
    }

    public void SetPosition(Transform nextPosition)
    {
        
        currentPosition = nextPosition.position;

    }
}
