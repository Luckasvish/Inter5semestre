using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Preparer : _InteractionOBJ
{
   
     FeedBackManager feedback {get;set;}
    public override _Item itenOnThis { get; set; }
    public override bool hasItemOnIt {get; set;}
    internal override Material material{get ; set;}  

  
    public Transform ingredientPosition;
    public float preparationTime;
    internal float preparationTimer;
    EventInstance choppSfxEvent;

    public GameObject knife;
    Vector3 knifeOriginialPosition;
    
    public Transform chefsHands;

    bool sfxPlayed;

    internal bool preparing;


    public PJ_Character chef;



    void Awake()
    {
        feedback = GetComponent<FeedBackManager>();
     
        itenOnThis = null;
   

    }
    void Start()
    {
        choppSfxEvent = RuntimeManager.CreateInstance("event:/SFX GAMEPLAY/sfx_chopp");
        material = GetComponent<MeshRenderer>().material;
        material.SetFloat("_emission", 4);
        knifeOriginialPosition = knife.transform.position;

    }
    void Update()
    {
        if(preparing)
        {
            Prepare();
            
            if( chef.input_Manager.moveInput.magnitude > 0.2 && chef.characterOn)
            {
                preparing = false;
            }


        }

    }
    public override void ReceiveItens(_Item itenInHand) // RECEBE O INGREDIENTE 
    {  
            itenOnThis = itenInHand;                         //  O ITEM DO PREPARER VIRA O ITEM QUE RECEBE
            itenOnThis.transform.position = ingredientPosition.position; // O ITEM VA PRA POSIÇÃO CORRETA
            itenOnThis.transform.SetParent(this.transform);
            preparationTimer = 0;                                       // O TIMER É ZERADO
            feedback.ToogleUI();
            RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_put");                                       //  A HUD INICIA 
            hasItemOnIt = true;                                         //TEM UM ITEM 
    }


    void Prepare()
    {
        preparationTimer += Time.deltaTime;
        knife.transform.position = chefsHands.position;

        if(sfxPlayed == false)
        {
            choppSfxEvent.start();
            sfxPlayed = true;
        }
        float _hudBar = preparationTimer / preparationTime;

        feedback.RunSlider(_hudBar);
        
        if(preparationTimer >= preparationTime)
        {
            itenOnThis.GetComponent<IngredientInstance>().CutMeat();
            itenOnThis.type = ItemType._PreparedIngredient;
            knife.transform.position = knifeOriginialPosition;
            preparing = false;
            choppSfxEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            sfxPlayed = false;
        }


    }

    public void TooglePreparer()
    {   
        preparing = !preparing;
    }


     public override _Item GiveItens(_Item itenToGive)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {    
            itenToGive = itenOnThis;
            itenOnThis = null;
            hasItemOnIt = false;
            feedback.ToogleUI();
            RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
            return itenToGive;
    }
    
    public override void Interact(_Item iten, PJ_Character chef)
    {
        if(iten !=null)
        {
            if(iten.type == ItemType._UnpreparedIngredient)
            {   
                if(hasItemOnIt == false) ReceiveItens(chef.GiveIten(chef.itenInHand));
                 else  Debug.Log("Já tem um item aqui!!!");
            }
             
            else if (iten.type == ItemType._PreparedIngredient) Debug.Log("Esse ingrediente já está preparado!!!!");////////  
              else Debug.Log("Esse item não vem aqui!!!!");////////
        }

        else 
        {
            if(hasItemOnIt)     
            {
                if(itenOnThis.type != ItemType._PreparedIngredient)
                {
                    preparing = true;
                } 
                else if (itenOnThis.type == ItemType._PreparedIngredient) chef.ReceiveItens(this); 

                else  Debug.Log("Cheogu onde não deveria!!!");////////
        
            }

            else Debug.Log("Não tem nada aqui!!!");
                
        }
    }
   
}
