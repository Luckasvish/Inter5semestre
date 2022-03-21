using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsManager : MonoBehaviour
{
    [SerializeField]
    MainPlayer main;

    internal static Storers Storer;
    internal static Balcon _Balcon;
    internal static GameObject trigger;
    internal string trigger_name;
    void OnTriggerEnter(Collider coli)
    {
        trigger = coli.gameObject;
        trigger_name = trigger.tag;

        if(trigger_name == "Store")
        {
            Storer = trigger.GetComponent<Storers>();
            main.chef.canRetriveIngredient = true;

        }

        if(trigger_name == "Balcon")
        {
            _Balcon = trigger.GetComponent<Balcon>();
            main.chef.nextToBalcon = true;


        }

    }
    
    void OnTriggerExit(Collider coli)
    {

        if(trigger_name == "Store")
        {
            main.chef.canRetriveIngredient = false;
            trigger_name = "";
            Storer = null;
            trigger = null;
        }

        if(trigger_name == "Balcon")
        {
            main.chef.nextToBalcon = false;
            trigger_name = "";
            _Balcon = null;
            trigger = null;
        }

    }
}
