using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    [SerializeField]
    internal MainPlayer main;

    internal bool canRetriveIngredient;
    internal bool canTryToPutIngredient;
    [SerializeField]
    internal bool canStartCutting;

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

        
        if(canTryToPutIngredient == true && Input.GetKeyDown(KeyCode.E))
        {
            CookIngredient(CollisionsManager.Oven);
        }
       
        if(canStartCutting == true && Input.GetKeyDown(KeyCode.E))
        {
            if (CollisionsManager._Board.hasMeat == false)
            {
                CollisionsManager._Board.hasMeat = true;
                CollisionsManager._Board._ingre = ItenInHand;
                ItenInHand = null;
            }
            else if (CollisionsManager._Board.hasMeat == true && CollisionsManager._Board.canGetPreparedMeat == true)
            {
                ItenInHand = CollisionsManager._Board.GetPreparedMeat();
            }
        }

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

    public void CookIngredient(Oven oven)
    {

        if(oven.cooking_Ingredients < 3)
        {
            Ingredientes ingre = ItenInHand.GetComponent<Ingredientes>();
            
            if(ingre.prepared == true)
            {
                oven.ReceiveIngredients(ingre);
                ItenInHand.gameObject.SetActive(false);
                ItenInHand = null;
            }
            else 
            {
                Debug.Log("O ingrediente não está preparado");
            }
        }
        
        else 
        {
            Debug.Log("A panela está cheia !!!");
        }

    }


}
