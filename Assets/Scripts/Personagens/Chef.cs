using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    [SerializeField]
    internal MainPlayer main;

    internal bool canRetriveIngredient;

    public static bool hasItem;
    public static Itens ItenInHand;

    public  Transform chefsHands;

    public bool nextToBalcon;

    float cd =2f;
    void Update()
    {
        if(canRetriveIngredient && Input.GetKeyDown(KeyCode.E))
        {
            
            GetIngredient();
          
        }
      
        if(nextToBalcon == true && Input.GetKeyDown(KeyCode.E) && cd <=0)
        {
            GetPlaceIten();
        }
        if(cd > 0) cd -= Time.deltaTime;


    }



    public void GetPlaceIten()
    {
        if(hasItem && CollisionsManager._Balcon.hasItem == false)
        {   

            ItenInHand.SetPosition(CollisionsManager._Balcon.SetItemPosition());
            CollisionsManager._Balcon.iten = ItenInHand;
            CollisionsManager._Balcon.hasItem = true;
            hasItem = false;
            ItenInHand = null;
            cd =2f;
        }

        else if(!hasItem && CollisionsManager._Balcon.hasItem == true)
        {
            ItenInHand = CollisionsManager._Balcon.iten; 
            CollisionsManager._Balcon.hasItem = false;
            hasItem = true;
            cd =2f;

        }
        

    }

    public void GetIngredient()
    {

        if(hasItem == false)
            {

                ItenInHand = CollisionsManager.Storer.GiveIngredient(CollisionsManager.Storer.code);
                Debug.Log(ItenInHand);
                hasItem = true;

            }

    }


}
