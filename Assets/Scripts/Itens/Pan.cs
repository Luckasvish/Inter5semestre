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
   
    internal bool burned; //Se queimou.
    float currentTime; // Tempo de cozimento.
    public float maxTime; // Tempo máximo de cozimento.

    public float _burnTime; // Tempo pra queimar.
    internal bool onOven ; 
    internal IngredientInstance ingredient;
    
    ////////////////////////////////////////////////////
    ////////////////////////////////////////////////////
    ////////////////////////////////////////////////////
    /// Itens ////////
    
    public GameObject ready;

    void Awake()    //INICIALIZqANDO A PAN
    {
        ready.SetActive(false);
        onOven = true;
        feedBack = GetComponent<FeedBackManager>();
        itemName = "Pan";
        type = ItemType._Pan;
    }

    
    void Update()
    {
        if(ingredient != null && onOven && !ready.activeInHierarchy)
        {
            Cook();
        }
        if(ready.activeInHierarchy)
        {
            BurnTime();
        }
    
    }
    void Cook()
    {
        currentTime += Time.deltaTime;
        float timer = currentTime / maxTime;
        feedBack.RunSlider(timer);
        if(currentTime >= maxTime)
        {
            ready.SetActive(true);
            currentTime =0;
        }
    }

    void BurnTime()
    {
        currentTime += Time.deltaTime;
        float timer = currentTime / _burnTime;
        feedBack.RunSlider(timer);
        if(currentTime >= _burnTime)
        {
            burned = true;
        }
    }

    public void ReceiveItens(IngredientInstance ingre)
    {
        ingredient = ingre;
        cooking = true;
        ingre.gameObject.SetActive(false);
        feedBack.ToogleUI();
    }

    public string GiveItem( string buffer)
    {
        if(burned == true)
        {
            buffer = ingredient.itemName;
            ingredient = null;
            feedBack.ToogleUI();
            ready.SetActive(false);
            return buffer;
        }

        else 
        {
            buffer = ingredient.itemName;
            ingredient = null;
            feedBack.ToogleUI();
            ready.SetActive(false);
            return buffer;
        }
    }

}
