using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    ///COMPONENTES DE PLAYER///
    internal Movement_Manager movement_Manager;
    internal Input_Manager input_Manager;

    [SerializeField]
    internal Detection_Manager detector;
    ///--------------------///


    ///CHEF ---
    public Itens itenInHand;
    bool hasItem;
    public Transform itenPosition;



    void Awake()
    {
        input_Manager = MacroSistema.sistema.input_Manager;
        movement_Manager = GetComponent<Movement_Manager>();
    }


    void Update()
    {
        bool actionButon = input_Manager.pressedE;
    
        if(actionButon)
        {


                if(detector.storerOnRange)
                {
                   
                    ReceiveItens(detector.closestStorer.GiveIngredient(), hasItem);
                }

                else if (detector.ovenOnRange)
                {
                    if (detector.closestOven.hasPan == true)
                    {
                        
                        if(hasItem == false)
                        {
                            if(detector.closestOven.Pan.recipeReady == true)
                            {
                                ReceiveItens(detector.closestOven.Pan.GiveRecipe());
                            }
                            else
                            {
                                ReceiveItens(detector.closestOven.GivePan(detector.closestOven.Pan));
                            }
                        
                        }
                        
                        else
                        {
                            detector.closestOven.Pan.ReciveIngredient(GiveIten());
                            hasItem = false;
                        }
                    }

                    else 
                    {
                        if(hasItem == false){ Debug.Log("Não tem panela aqui!");}
                        else if (hasItem == true)
                        {
                            detector.closestOven.ReceivePan(GiveIten());
                            hasItem = false ;
                        }
                    }
                }
                else if (detector.preparerOnRange)
                {
                    
                    if(detector.closestPreparer.hasIngredientOnIt == false)
                    {

                        detector.closestPreparer.ReceiveIngredients(GiveIten());
                        hasItem = false;
                    }
                    else
                    {
                        ReceiveItens(detector.closestPreparer.GivePreparedIngredient(itenInHand));
                    }

                }
                else if (detector.balconOnRange)
                {
                    if(detector.closestBalcon.hasItemOnIt == true)
                    {
                        ReceiveItens(detector.closestBalcon.GivesIten(itenInHand));
                    }
                    else 
                    {
                        detector.closestBalcon.ReceivesItens(GiveIten());
                        hasItem = false;
                    }
                }

                else 
                {
                    Debug.Log("No Action!!!");
                }
        }
    
        if(hasItem == true)
        {
            itenInHand.transform.position = itenPosition.position;
        }
        else
        {
            itenInHand = null;
        }

        Debug.Log(hasItem);    
    }

    void FixedUpdate()
    {
        movement_Manager.Move(input_Manager.moveInput);
    }



    Itens GiveIten()//Função de dar Item.
    {   
        return itenInHand;
    }

    void ReceiveItens(Itens itensReceived)//Função de receber itens, recebendo o item como parâmetro.
    {
            hasItem = true;
            itenInHand = itensReceived;
    
    }
     void ReceiveItens(Ingredientes ingredientesReceived, bool _hasItem)//Função de receber itens, recebendo o item como parâmetro.
    {
        if(_hasItem == true)
        {
            Debug.Log("Já possui item na mão : " + itenInHand);
        }

        else 
        {
            itenInHand = ingredientesReceived;
            hasItem = true;
        }
    }

    
}
