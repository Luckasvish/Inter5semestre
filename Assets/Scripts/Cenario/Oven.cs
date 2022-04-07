using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Interagibles
{
    public override InteragibleType type { get; set;}
    
    public override Itens itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}

    internal bool cooked;
    public Transform PanPosition;

    void Awake()
    {
        type = InteragibleType._Oven;
    }

    void Start()
    {
        itenItHas  = GetComponentInChildren<Pan>();
        itenItHas.transform.position = PanPosition.position;
        hasItemOnIt = true;
       
    }

    public override Itens GiveItens(Itens Buffer)
    {
       if(cooked == false)
       {
            Buffer = itenItHas;
            itenItHas = null;
            hasItemOnIt = false;
            return Buffer;
       }
       
       else
       {
           return itenItHas.GetComponent<Pan>().GiveRecipe();
       }

    }
    public override void ReceiveItens(Itens _Pan)
    {
            
                itenItHas = _Pan;
                itenItHas.transform.position = PanPosition.position;
                hasItemOnIt = true;
       
    }
    
    public void PutIngredient(Itens itenInHand)
    {
        Pan _Pan = itenItHas.GetComponent<Pan>();

        if(_Pan != null)
        {
            _Pan.ReciveIngredient(itenInHand);
        }
        else
        {
            Debug.Log("Pan não está aqui"); ///
        } 
    }
}
