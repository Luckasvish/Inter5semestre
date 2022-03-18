using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    [SerializeField]
    internal Controlador controlador;

    internal bool canRetriveIngredient;


    void Update()
    {
        if(canRetriveIngredient && Input.GetKeyDown(KeyCode.E))
        {
            controlador.collisions.ingredient_Storer.GiveIngredient(controlador.collisions.ingredient_Storer.code);
        }


    }

}
