using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Interactable
{
    public override InteractableType type { get; set;}
    FeedBackManager feedback {get;set;}
    
    public override Itens itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    public override GameObject highLight { get ; set ; }
    public override bool highLightOn {get; set;}

    public Transform PanPosition;

    public Pan _Pan;

    void Awake()
    {
        type = InteractableType._Oven;
         highLight = GetComponentInChildren<Light>().gameObject;
        highLight.SetActive(false);
        
    }

    void Update()
    {
        if(highLightOn)
      {
        highLight.SetActive(true);
      }
      else 
      {
        highLight.SetActive(false);
      }
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
                }
                
                itenItHas.transform.position = PanPosition.position;
                hasItemOnIt = true;
       
    }
    
}
