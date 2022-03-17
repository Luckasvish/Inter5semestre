using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    //TROCA DE PERSONAGENS
    public static string activePerson;
    public PJ_Controller[] Persons;
    int changeControl;
    public float changeCD;
    float changeCD_Timer;

    void Start()
    {
        changeControl = 0;
        activePerson = Persons[0].personName;
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space) && changeCD_Timer <= 0)
        {
            ChangePerson();
        }

        if(changeCD > 0)
        {
            changeCD_Timer -= Time.deltaTime;
        }
        

    }

    void ChangePerson()
    {

        if (changeControl == 0)
        {
            activePerson = Persons[1].personName;
            changeControl = 1;
            changeCD_Timer = changeCD;
        }

        else
        {   
            activePerson = Persons[0].personName;
            changeControl = 0;
            changeCD_Timer = changeCD;
        }


    }

}
