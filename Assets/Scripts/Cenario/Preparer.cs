using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Preparer : Interactable
{
    public override InteractableType type { get; set;}
     FeedBackManager feedback {get;set;}
    public override Itens itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    public override GameObject highLight { get ; set ; }
    public override bool highLightOn {get; set;}
    public Transform ingredientPosition;
    public float preparationTime;
    internal float preparationTimer;

    bool preparing;

    void Awake()
    {
        feedback = GetComponent<FeedBackManager>();
        type = InteractableType._Preparer;
        itenItHas = null;
        highLight = GetComponentInChildren<Light>().gameObject;
        highLight.SetActive(false);   
    }
    
    void Update()
    {
        if(preparing)
        {
            Prepare();
        }
        if(highLightOn)
      {
        highLight.SetActive(true);
      }
      else 
      {
        highLight.SetActive(false);
      }   
    }
    
    public override void ReceiveItens(Itens itenInHand) // RECEBE O INGREDIENTE 
    {  
            itenItHas = itenInHand;                         //  O ITEM DO PREPARER VIRA O ITEM QUE RECEBE
            itenItHas.transform.position = ingredientPosition.position; // O ITEM VA PRA POSIÇÃO CORRETA
            preparationTimer = 0;                                       // O TIMER É ZERADO
            feedback.ToogleHUD();                                       //  A HUD INICIA 
            hasItemOnIt = true;                                         //TEM UM ITEM 
    }


    void Prepare()
    {
        preparationTimer += Time.deltaTime;
        float _hudBar = preparationTimer / preparationTime;
        feedback.Run(_hudBar);
        if(preparationTimer >= preparationTime)
        {
            itenItHas.type = ItenType._PreparedIngredient;
            preparing = false;
        }
    }

    public void TooglePreparer()
    {   
        preparing = !preparing;
    }


     public override Itens GiveItens(Itens itenToGive)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {    
            itenToGive = itenItHas;
            itenItHas = null;
            hasItemOnIt = false;
            feedback.ToogleHUD();
            return itenToGive;
    }
    
   
}
