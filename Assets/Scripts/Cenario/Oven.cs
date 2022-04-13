using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Interactable
{
    public override InteractableType type { get; set;}
    public override FeedBackManager feedback {get;set;}
    
    public override Itens itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    public override GameObject highLight { get ; set ; }

    public Transform PanPosition;

    public Pan _Pan;

    void Awake()
    {
        type = InteractableType._Oven;
    }

    public override void TurnHighLightOn()
    {
      highLight.SetActive(true);
    }
    void Start()
    {
        _Pan  = GetComponentInChildren<Pan>();
        itenItHas = _Pan;
        itenItHas.transform.position = PanPosition.position;
        hasItemOnIt = true;
    }


    public override Itens GiveItens(Itens Buffer)
    {
       
            Buffer = itenItHas;
            itenItHas = null;
            hasItemOnIt = false;
            if(_Pan.cooking == true)
            {
                _Pan.cooking = false;
                _Pan.feedBack.ToogleHighLight();
            }
            _Pan = null;
            return Buffer;
       
    }

    public override void ReceiveItens(Itens Pan)
    {
                _Pan = Pan.GetComponent<Pan>();
                itenItHas = _Pan;
                if(_Pan.ingreIn > 0)
                {
                    _Pan.cooking = true;
                    _Pan.feedBack.ToogleHighLight();
                }
                
                itenItHas.transform.position = PanPosition.position;
                hasItemOnIt = true;
       
    }
    
}
