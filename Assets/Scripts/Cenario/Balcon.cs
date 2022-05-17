using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balcon : Interactable
{
    public override InteractableType type { get; set;}

    public override Item itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    public  GameObject highLight;
    public override bool highLightOn {get; set;}
    public Transform itemPosition;
    
    internal bool havePlate;
    
    
        void Awake()
    {
        type = InteractableType._Balcon;
        if(GetComponentInChildren<Plates>() == null)
        {
            itenItHas = null;
        }
        else
        {
          hasItemOnIt=true;
          itenItHas = GetComponentInChildren<Plates>();
        }
        highLightOn = false;
        highLight.SetActive(false);
    }

    void Update()
    {

      if(hasItemOnIt)
      {
        if(itenItHas.type == ItemType._Plate)
        {
          Plates plate = itenItHas.GetComponent<Plates>();

            if(highLightOn)
            {
              plate.hud.SetActive(true);
            }
            else
            {
              plate.hud.SetActive(false);
            }
        }
        else 
        {
            if(highLightOn)
            {
              highLight.SetActive(true);
            }
            else
            {
              highLight.SetActive(false);
            }
        }
      }
      else
      {
          highLight.SetActive(false);
      }
    }


    public override void ReceiveItens(Item itenInHand)
    {
        itenItHas = itenInHand;
        itenItHas.transform.position = itemPosition.position;
        hasItemOnIt = true;
    }

    public override Item GiveItens(Item itenToGive)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {
        itenToGive = itenItHas;
        itenItHas = null;
        hasItemOnIt = false;
        return itenToGive;
    }

     public override void Interact(Item iten, Chef chef)
     {
        if(iten == null)
        {
           if(hasItemOnIt) chef.ReceiveItens(this);
            else Debug.Log("Não tem item aqui!");///
        } 

        else 
        {
          if(hasItemOnIt == true)
          {
           
           if( iten.type == ItemType._Pan)
            {
                
                if(itenItHas.type == ItemType._Plate )
                {
                    Pan pan = iten.GetComponent<Pan>();
                    Plates plate = itenItHas.GetComponent<Plates>();
                    
                    if(plate != null && pan != null)
                    {
                      if(plate.CheckIngredient(pan.ingredient.itemName) == true)
                      {
                        plate.ReceiveIngredient(pan.GiveItem(pan.ingredient.itemName));
                      }
                      else
                      {
                           Debug.Log("Esse ingrediente não entra nessa receita!!!!");///////////////////
                      }
                    }  
                }
                else 
                {
                  Debug.Log("Já tem um iten neste balcão !!!");///////////////////
                }
            }
            else
            {
              Debug.Log("Já tem um iten neste balcão !!!");////////////////////
            } 

          }
          else ReceiveItens(chef.GiveIten(chef.itenInHand));
        }
     }
}
