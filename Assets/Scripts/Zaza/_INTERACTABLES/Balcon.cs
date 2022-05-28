using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Balcon : _InteractionOBJ
{
    public override InteractableType type { get; set;}

    public override _Item itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    internal override Material material{get ; set;}  

    
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
      
    }
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        material.SetFloat("_emission", 4);
    }

    void Update()
    {
      

      if(hasItemOnIt)
      {
        if(itenItHas.type == ItemType._Plate)
        {
          Plates plate = itenItHas.GetComponent<Plates>();



        }
      }
    }


    public override void ReceiveItens(_Item itenInHand)
    {
        itenItHas = itenInHand;
        itenItHas.transform.position = itemPosition.position;
        RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_put");
        hasItemOnIt = true;
    }

    public override _Item GiveItens(_Item itenToGive)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {
        itenToGive = itenItHas;
        itenItHas = null;
        hasItemOnIt = false;
        RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
        return itenToGive;
    }

     public override void Interact(_Item iten, Chef chef)
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
