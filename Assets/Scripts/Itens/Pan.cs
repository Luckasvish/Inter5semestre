using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Item
{
    public  override ItemType type { get; set; }
    public  override string itemName { get; set; }
    //////////////////////////////////////////////////////
    public FeedBackManager feedBack;
  
    //////////////////////////////////////////////////////
    ////////////////// COZINHAR ////////////////////////////
    /////////////////////////////////////////////////////

    internal bool cooking; // Cozinhando.
    internal bool recipe; // Se a receita foi formada.
    internal bool burned; //Se queimou.
    float currentTime; // Tempo de cozimento.
    float maxTime; // Tempo máximo de cozimento.
    internal int ingreIn; // Quantidade de ingredientes.
    public float stage00; //Tempo máximo de cozimento da primeira etapa.
    public float stage01; //Tempo máximo de cozimento da segunda etapa.
    public float stage02;   //Tempo máximo de cozimento da terceira etapa.  
    public float BurnTime; // Tempo pra queimar.

    ////////////////////////////////////////////////////
    ////////////////////////////////////////////////////
    ////////////////////////////////////////////////////
    /// Itens ////////
    Item item00;
    Item item01;
    Item item02;

    void Awake()    //INICIALIZANDO A PAN
    {
        feedBack = GetComponent<FeedBackManager>();
        itemName = "";
        type = ItemType._Pan;
    }

    
    void Update()
    {
        if(cooking)
        {
            Cook();
        }

        if(item00 != null) Debug.Log(item00.itemName); ///
        if(item01 != null) Debug.Log(item01.itemName); ///
        if(item02 != null) Debug.Log(item02.itemName); ///
    }


    public void CheckIngredient(Item _ingre)
    {
        if(item00 == null)
        {
            item00 = _ingre;
            ingreIn ++;
        }

        else if(item00 != null && item01 == null)
        {   
            item01 = _ingre;
            ingreIn ++;
        }
        else if(item00 != null && item01 != null && item02 == null)
        {   
            item02 = _ingre;
            ingreIn ++;
        }
        else
        {   
            Debug.Log("Panela cheia");
        }
        
       


        if(CanAddIngredient(_ingre))
        {
            _ingre.gameObject.SetActive(false);
            cooking = true;
        }
    }


    void Cook()
    {
        currentTime += Time.deltaTime;
        float timer = currentTime / maxTime;
        feedBack.Run(timer);
        if(currentTime >= maxTime)
        {
                float buffer = maxTime + BurnTime;
                Debug.Log(buffer);///

                if(currentTime >= buffer)
                {
                    burned = true;
                    cooking = false;
                }
        }
    }

    void AddIngredient()
    { 
        switch(ingreIn)
        {
            case 0: 
                maxTime = stage00;  
                ingreIn ++;
                feedBack.ToogleHUD();
                cooking = true;
                Debug.Log("Chegou 00!!");//////////////
            break;

            case 1:
                maxTime+= stage01;
                ingreIn ++;
            break;

            case 2:
                maxTime += stage02;
                recipe = true;
            break;
        }    
    
    }

    public Item GiveRecipe(Plates plate)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {
        if(burned == false)
        {
            feedBack.ToogleHUD();
            cooking = false;
            return MacroSistema.sistema.SpawnRecipe(plate);
        }

        else
        {
            feedBack.ToogleHUD();
            cooking = false;
            return null;
        }
    }




     bool CanAddIngredient(Item ingre)
     {
          switch(itemName)
            {
                case "": {AddIngredient();Debug.Log("Chegou 01!!!!");return true;}
                
                case "A":   if(ingre.name == "C" || ingre.name == "F")  {AddIngredient();return true;}         ///COMEÇANDO POR ARROZ 
                            else return false;
                case "AC":  if(ingre.name == "F")  {AddIngredient();return true;}                      // SE COLOCOU CARNE COLOCA O FEIJÂO 
                            else return false;
                case "AF":  if(ingre.name == "C")  {AddIngredient();return true;}                     // SE COLOCOU FEIJÂO COLOCA A CARNE 
                            else return false;



                case "F":   if(ingre.name == "F" || ingre.name == "C" || ingre.name == "A")  {AddIngredient();return true;}      ///COMEÇANDO POR FEIJÃO
                            else return false;
                case "FF":  if(ingre.name == "C")  {AddIngredient();return true;}         // SE COLOCOU FEIJÃO COLOCA A CARNE
                            else return false;
                case "FC":  if(ingre.name == "F" || ingre.name == "A")  {AddIngredient();return true;}          // SE COLOCOU CARNE COLOCA FEIJÃO OU ARROZ
                            else return false;
                case "FA" : if(ingre.name == "C")  {AddIngredient();return true;}         // SE COLOCOU ARROZ COLOCA CARNE
                            else return false;


                case "C":  {AddIngredient();return true;}                                  ///COMEÇANDO COM CARNE
                case "CA": if (ingre.name == "F")  {AddIngredient();return true;}          //SE COLOCOU ARROZ COLOCA FEIJÃO
                            else return false;
                case "CF": if (ingre.name == "A")  {AddIngredient();return true;}          //SE COLOCOU FEIJÃO COLOCA ARROZ
                            else return false;
                case "CFa" : if (ingre.name == "C")  {AddIngredient();return true;}       //SE COLOCOU FAROFA COLOCA CARNE
                            else return false;
                case "CC" : if(ingre.name == "Fa")  {AddIngredient();return true;}       //SE COLOCOU CARNE COLOCA FAROFA
                            else return false;


                case "Fa": if(ingre.name == "C") {AddIngredient();return true;}
                            else return false;
                case "FaC" : if(ingre.name == "C")   {AddIngredient();return true;}
                            else return false;

                default : return false;
            }
     }






}
