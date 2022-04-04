using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Itens : MonoBehaviour
{   
    internal Chef chef; 

    void Awake()
    {
        chef = FindObjectOfType<Chef>();
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
