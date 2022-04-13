using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public static Chair instance;
    public GameObject Food;
    private bool hasFood;



    private void Awake()
    {
        instance = this;
    }

    public bool CheckIfHasFood()
    {
        if(Food != null)
            hasFood = true;
        else 
            hasFood = false;

        return hasFood;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
