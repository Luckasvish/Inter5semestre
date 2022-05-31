using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
public class Pan : _Item
{
    // PROPRIEDADES DE ITEM
    public  override ItemType type { get; set; }
    public  override string itemName { get; set; }
    //////////////////////////////////////////////////////
  
    // PROPRIEDADES DE PAN PRA COZINHAR
    internal bool onOven ;  //  booleana pra saber se está no fogão.
    internal float currentTime; // Tempo de cozimento.
    internal IngredientInstance ingredient; // referência do ingrediente da panela.
    
    


            //////////  COCKING //////////
    bool startedCooking;    // booleana para incializar o cozimento.
    public float _cockingTime; // Tempo máximo de cozimento.
    internal bool finishedCooking; // Terminou de cozinhar.




            //////////  BURNING //////////
    bool startedBurning;    // booleana para inicializar a queima.
    public float _burnTime; // Tempo pra queimar.

    /////////////////////////////////////////////////////
    
    // PROPRIEDADES ADICIONAIS DE PAN       
    internal PanTimer Timer;    //  Referência pro timer. 
    internal Vector3 panPosition;   //  posição passada pros fogões para ser colocada.
    ////////////////////////////////////////////////////
    
    void Awake()   
    {
        panPosition = this.transform.position;     // Setando o valor da posição da panela (passa pra Oven posicionar).
        
        if(GetComponentInParent<Oven>() != null) onOven = true; //  (se tiver um fogão em Parent) {a panela está no fogão}
        
        type = ItemType._Pan;  
    }

    
    void Update()
    { 
        if(ingredient != null && onOven)    Cook();  //  (se tiver um ingrediente na panela e ela estiver no fogão) { Cozinhar(); }
        
        else if (ingredient != null && onOven == false)
        {
            if(ingredient.type == ItemType._PreparedIngredient && startedCooking == true)
            {
                StopCooking(false);
            }
            else if(ingredient.type == ItemType._CookedIngredient && startedBurning == true)
            {
                StopBurn(false);
            }

        }
        
    
    }


    //Método que trata o cozimento.
    void Cook() 
    {
        if(ingredient.type == ItemType._PreparedIngredient) //  PATH #1 : (se o ingrediente for do tipo preparado)
        {
            
            if(startedCooking == false ) { StartCooking();}  // (se não tiver começado a cozinhar) { Começa a cozinhar() }

            else currentTime += Time.deltaTime; //  (se já tiver começado a cozinhar)  { O tempo de cozimento começa a aumentar}
               
            if(currentTime >= _cockingTime &&  startedCooking == true)   {StopCooking(true);}    // (se o tempo atual for maior que o tempo de cozimento )   {Para de cozinhar()}

        }


        else if(ingredient.type == ItemType._CookedIngredient)   //  PATH #2 : (se o ingrediente for do tipo cozinhado)
        {
            
            if(startedBurning == false) StartBurning(); // (se não tiver começado a queimar) { Começa a queimar()}
            
            else currentTime += Time.deltaTime; //  (se já tiver começado a queimar)  { O tempo de queima começa a aumentar}
           
            if(currentTime >= _burnTime && startedBurning == true) {StopBurn(true);}    // (se o tempo atual for maior que o tempo de queima )   {Para de queimar()}

        }
    
    }

    //Método para começar a queimar 
    public void StartBurning()
    {   
        if(currentTime != 0)
        {
            Timer.cookingSfxEvent.start();
            Timer.cookingSfxEvent.setParameterByName("fire_on", 1);
            Timer.ToogleTimer();
        }
        
        Timer.CheckImageToSet();
        startedBurning = true;    
        Timer.timer.color = Timer.red;
    }

    
    //  Método para parar de queimar (chamado quando queima ou quando a panela sai do fogão)
    void StopBurn( bool burned) 
    {
        if(burned == true)  //  (se a comida queimar)
        {
            ingredient.type = ItemType._BurnedIngrediente;  //  { Ela vira ingrediente queimado }
            Timer.burningSfxEvent.setParameterByName("burn", 1);
            Timer.burningSfxEvent.start();
            Timer.CheckImageToSet();
        }
        else 
        {
            Timer.ToogleTimer();
        }
        
        Timer.cookingSfxEvent.setParameterByName("fire_on", 0);
        startedBurning = false; //  e começou a queimar fica falso
    }




    //Método pra começar a cozinhar.
    public void StartCooking()
    {
        Timer.ToogleTimer();       //   Liga o Timer.
        Timer.cookingSfxEvent.start();
        Timer.cookingSfxEvent.setParameterByName("fire_on", 1);
        startedCooking = true;  // e começa a cozinhar.
        Timer.timer.color = Timer.green;

    }

    //Método para parar de cozinhar (recebe uma booleana para dizer se o ingrediente já está pronto ou não).
    public void StopCooking(bool endCooking)
    {
        if(endCooking)  //  (se o ingrediente estiver pronto)
        {
            currentTime = 0;
            ingredient.type = ItemType._CookedIngredient;   //  {o ingrediente vira do tipo cozido}
            
            finishedCooking = true; //  e a panela cozinhou 
            
            
        }
        else 
        {
            Timer.ToogleTimer();
            Timer.cookingSfxEvent.setParameterByName("fire_on", 0);
        }

        startedCooking = false;
        
    }

    

 
    public void ReceiveItens(IngredientInstance ingre)
    {
        ingredient = ingre;
        ingre.gameObject.SetActive(false);
        RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
    }

    public string GiveItem( string buffer)
    {
        
            buffer = ingredient.itemName;
            ingredient = null;
            if(onOven == true)
            {
                Timer.ToogleTimer();    
            }
            RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
            return buffer;
        
    }

    public void Position(){ transform.position = panPosition;}  //  Método pra posicionar a panela.
    
   

}
