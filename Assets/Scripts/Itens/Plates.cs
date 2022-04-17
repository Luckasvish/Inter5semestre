using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Plates : Item
 
 {

    public  override ItemType type { get; set; }
    public  override string itemName { get; set; }
   
     public GameObject hud;
    /////////////////////////////////////////////////////////////////////////
     public GameObject[] feijoada;
     public GameObject[] pratoFeito;
      public GameObject[] buchada;


    //////////////////////////////////////////////////////////////////

    internal Recipes recipe;
    bool settle;
   
    //////////////////////////////////////////////////////////
    public OrderHUD mainHUD;

    void Awake()
    {
        Balcon balcon = GetComponentInParent<Balcon>();
        
        if(balcon != null)
        {
            balcon.itenItHas = GetComponent<Plates>();
            balcon.hasItemOnIt = true;
        }
        type = ItemType._Plate;

        recipe = new Recipes("Feijoada");
    }
    public void SpanwRecipeOnPlate()
    {

        switch(recipe.itemName)
        {

                case "Feijoada":
                    
                    for(int i = 0; i< feijoada.Length; i++)
                    {
                        Color cor =  feijoada[i].GetComponent<MeshRenderer>().material.color;
                        cor.a = 0.25f;
                        feijoada[i].SetActive(true);
                        pratoFeito[i].SetActive(false);
                        buchada[i].SetActive(false);
                    }
                    
                                break;
                 case "PratoFeito":
                        
                        for(int i = 0; i< feijoada.Length; i++)
                    {
                        feijoada[i].SetActive(false);
                        pratoFeito[i].SetActive(true);
                        buchada[i].SetActive(false);
                    }
                    
                                break;
                  case "Buchada":
                        for(int i = 0; i< feijoada.Length; i++)
                    {
                        feijoada[i].SetActive(false);
                        pratoFeito[i].SetActive(false);
                        buchada[i].SetActive(true);
                    }
                    
                                break;

        }



    }
    void Update()
    {
        if(recipe != null)
        {
             Debug.Log("Receita : " + recipe.itemName);//////
            mainHUD.SetReciepOrderHUD(recipe.itemName);
        
        }
    }

    public void ReceiveIngredient(IngreType ingre) 
    {
       
            switch(recipe.itemName)
            {
                case "Feijoada":    

                    if(ingre == IngreType._Beans)
                    {
                        if(feijoada[0].activeInHierarchy == false)
                        {
                            feijoada[0].SetActive(true);
                        }

                        else if(feijoada[1].activeInHierarchy == false)
                        {
                            feijoada[1].SetActive(true);
                        }
                        else 
                        {
                            Debug.Log("Já tem feijão demais aqui!!");////////////////////////////////////////////////////
                        }
                    }
  

                    else if(ingre == IngreType._Meat)
                    {
                        if(feijoada[2].activeInHierarchy == false)
                        {
                            feijoada[2].SetActive(true);
                        }
                        else
                        {
                            Debug.Log("Já tem carne demais aqui!!");////////////////////////////////////////////////////
                        }
                    }
                     else 
                    {
                        Debug.Log("Esse ingrediente não entra nesse prato!!!");//////////////////
                    }
                    break;
                
                case "PratoFeito": 
                    
                    if(ingre == IngreType._Beans)
                    {
                        if(pratoFeito[0].activeInHierarchy == false)
                        {
                            pratoFeito[0].SetActive(true);
                        }
                        else 
                        {
                            Debug.Log("Já tem feijão demais aqui!!");////////////////////////////////////////////////////
                        }
                    }
                    else if (ingre == IngreType._Rice)
                    {   
                        if(pratoFeito[1].activeInHierarchy == false)
                        {
                            pratoFeito[1].SetActive(true);
                        }
                        else 
                        {
                            Debug.Log("Já tem arroz demais aqui!!");////////////////////////////////////////////////////
                        }
                    }
                     else if (ingre == IngreType._Meat)
                    {   
                        if(pratoFeito[2].activeInHierarchy == false)
                        {
                            pratoFeito[2].SetActive(true);
                        }
                        else 
                        {
                            Debug.Log("Já tem carne demais aqui!!");////////////////////////////////////////////////////
                        }
                    }
                     else 
                    {
                        Debug.Log("Esse ingrediente não entra nesse prato!!!");//////////////////
                    }
                    break;
                
                case "Buchada":
                    
                    if (ingre == IngreType._Meat)
                    {   
                        if(buchada[0].activeInHierarchy == false)
                        {
                            buchada[0].SetActive(true);
                        }
                        else if(buchada[1].activeInHierarchy == false)
                        {
                            buchada[1].SetActive(true);
                        }
                        else 
                        {
                            Debug.Log("Já tem feijão demais aqui!!");////////////////////////////////////////////////////
                        }
                    }
                    else if (ingre == IngreType._Farofa)
                    {   
                        if(buchada[3].activeInHierarchy == false)
                        {
                            buchada[3].SetActive(true);
                        }
                    }
                    
                    else 
                    {
                        Debug.Log("Esse ingrediente não entra nesse prato!!!");//////////////////
                    }
                    break;

            }

    }

    
 }