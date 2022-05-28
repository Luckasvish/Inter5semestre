using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Preparer : _InteractionOBJ
{
    public override InteractableType type { get; set;}
     FeedBackManager feedback {get;set;}
    public override _Item itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    internal override Material material{get ; set;}  

  
    public Transform ingredientPosition;
    public float preparationTime;
    internal float preparationTimer;
    EventInstance choppSfxEvent;

   

    bool sfxPlayed;

    bool preparing;


    void Awake()
    {
        feedback = GetComponent<FeedBackManager>();
        type = InteractableType._Preparer;
        itenItHas = null;
   

    }
    void Start()
    {
        choppSfxEvent = RuntimeManager.CreateInstance("event:/SFX GAMEPLAY/sfx_chopp");
        material = GetComponent<MeshRenderer>().material;
        material.SetFloat("_emission", 4);
    }
    void Update()
    {
        if(preparing)
        {
            Prepare();
        }

    }
    public override void ReceiveItens(_Item itenInHand) // RECEBE O INGREDIENTE 
    {  
            itenItHas = itenInHand;                         //  O ITEM DO PREPARER VIRA O ITEM QUE RECEBE
            itenItHas.transform.position = ingredientPosition.position; // O ITEM VA PRA POSIÇÃO CORRETA
            preparationTimer = 0;                                       // O TIMER É ZERADO
            feedback.ToogleUI();
            RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_put");                                       //  A HUD INICIA 
            hasItemOnIt = true;                                         //TEM UM ITEM 
    }


    void Prepare()
    {
        preparationTimer += Time.deltaTime;
        if(sfxPlayed == false)
        {
            choppSfxEvent.start();
            sfxPlayed = true;
        }
        float _hudBar = preparationTimer / preparationTime;
        feedback.RunSlider(_hudBar);
        if(preparationTimer >= preparationTime)
        {
            itenItHas.type = ItemType._PreparedIngredient;
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
            itenToGive = itenItHas;
            itenItHas = null;
            hasItemOnIt = false;
            feedback.ToogleUI();
            RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
            return itenToGive;
    }
    
    public override void Interact(_Item iten, Chef chef)
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
                if(itenItHas.type != ItemType._PreparedIngredient) TooglePreparer(); 
                
                 else chef.ReceiveItens(this);  
            }
            else Debug.Log("Não tem nada aqui!!!");
                
        }
    }
   
}
