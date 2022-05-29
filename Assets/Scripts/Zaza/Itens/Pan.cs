using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
public class Pan : _Item
{
    // PROPRIEDADES DE ITEM
    public  override ItemType type { get; set; }
    public  override string itemName { get; set; }
    //////////////////////////////////////////////////////
  
    // PROPRIEDADES DE PAN
    internal bool cooking; // Cozinhando.
    internal bool burned; //Se queimou.
    float currentTime; // Tempo de cozimento.
    public float maxTime; // Tempo máximo de cozimento.
    public float _burnTime; // Tempo pra queimar.
    internal bool onOven ; 
    internal IngredientInstance ingredient;
    internal Vector3 panPosition;
    /////////////////////////////////////////////////////

    //UX / UI
    public FeedBackManager feedBack;
    EventInstance cookingSfxEvent;
    EventInstance burningSfxEvent;
    bool sfxPlayed;
    bool sfx1Played;
    public GameObject ready;

    void Awake()   
    {
        panPosition = this.transform.position;     // Setando o valor da posição da panela (passa pra Oven posicionar).
        ready.SetActive(false);
        onOven = true;
        feedBack = GetComponent<FeedBackManager>();
        itemName = "Pan";
        type = ItemType._Pan;
    }

    void Start()
    {
        cookingSfxEvent = RuntimeManager.CreateInstance("event:/SFX GAMEPLAY/sfx_cooking");
        burningSfxEvent = RuntimeManager.CreateInstance("event:/SFX GAMEPLAY/sfx_burning");
    }
    
    void Update()
    {
        if(ingredient != null && onOven && cooking == true)
        {
            Cook();
        }
        else 
        {
            if(sfxPlayed == true)
            {
                cookingSfxEvent.setParameterByName("fire_on", 0);
                cookingSfxEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);////////////////////testar fade ou imediato 
                sfxPlayed = false;
            }
            if(sfx1Played == true)
            {
                burningSfxEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);////////////////////testar fade ou imediato 
                sfx1Played = false;
            }
        }
        if(ready.activeInHierarchy)
        {
            BurnTime();
        }
    
    }
    void Cook()
    {
        if(sfxPlayed == false)
        {
            cookingSfxEvent.start();
            cookingSfxEvent.setParameterByName("fire_on", 1);
            sfxPlayed = true;
        }
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
        
        if(sfx1Played == false)
        {
            burningSfxEvent.start();
            sfx1Played = true;
        }
        currentTime += Time.deltaTime;
        float timer = currentTime / _burnTime;
        feedBack.RunSlider(timer);
        if(currentTime >= _burnTime)
        {
            burningSfxEvent.setParameterByName("burn", 1);
            burned = true;
        }
    }

    public void ReceiveItens(IngredientInstance ingre)
    {
        ingredient = ingre;
        cooking = true;
        ingre.gameObject.SetActive(false);
        RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
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
            RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
            return buffer;
        }

        else 
        {
            buffer = ingredient.itemName;
            ingredient = null;
            feedBack.ToogleUI();
            ready.SetActive(false);
            RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
            return buffer;
        }
    }

    public void Position(){ transform.position = panPosition;}  //  Método pra posicionar a panela.

}
