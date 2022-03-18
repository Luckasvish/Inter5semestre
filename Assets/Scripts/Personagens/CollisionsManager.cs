using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsManager : MonoBehaviour
{
    [SerializeField]
    Controlador controller;

    internal Storers ingredient_Storer;

    void OnTriggerEnter(Collider coli)
    {

        if(coli.gameObject.CompareTag("Store"))
        {
            controller.chef.canRetriveIngredient = true;
            ingredient_Storer = coli.gameObject.GetComponent<Storers>();
        }

    }
    
    void OnTriggerExit(Collider coli)
    {

        if(coli.gameObject.CompareTag("Store"))
        {
            controller.chef.canRetriveIngredient = false;
        }

    }
}
